using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using XmlTester.Interfaces;
using XmlTester.src;
using XmlTester.UIControls.VisualTreeElements;
using XmlTester.UIControls.VisualTreeElements.QueuedActions;

namespace XmlTester.UIControls
{
    /// <summary>
    /// Interaction logic for RuleTreeControl.xaml
    /// 
    /// Interface required by RuleTreeControl
    /// Select node by string path
    /// Update the tree according to the datacontext
    /// </summary>
    public partial class RuleTreeControl : UserControl, IRuleTreeView
    {
        public RuleViewer View { get; set; }
        public Dictionary<string, TreeViewItem> NodeMap { get; set; }
        public Dictionary<string, ITreeElement> PreviousState { get; set; }
        public ITreeElement Root { get; set; }
        public XmlNode XmlRoot { get; set; }
        public Stack<ITreeViewQueuedAction> ActionQueue { get; set; }

        public RuleTreeControl(RuleViewer View)
        {
            InitializeComponent();
            this.View = View;
            this.XmlRoot = View.testCase.TransformedDocument.Root;
            // Create the virtual tree
            Boolean.TryParse(View.testCase.GetOption("DefaultExpanded"), out bool expanded);
            this.Root = new NodeVisualTreeElement(this.XmlRoot, 0, expanded);
            docTreeViewer.ItemsSource = new List<ITreeElement>() { Root };
            NodeMap = new Dictionary<string, TreeViewItem>();
            PreviousState = new Dictionary<string, ITreeElement>();
            ActionQueue = new Stack<ITreeViewQueuedAction>();
        }

        private void ListViewScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void TreeViewItem_Loaded(object source, RoutedEventArgs e)
        {
            // tw is the TreeViewItem that gets loaded
            TreeViewItem tw = source as TreeViewItem;
            if (tw != null)
            {
                ITreeElement xNode = tw.DataContext as ITreeElement;
                if (xNode != null && (xNode.Node.NodeType == XmlNodeType.Element || xNode.Node.NodeType == XmlNodeType.Attribute))
                {
                    string path = Utils.getFullPath(xNode.Node);
                    // add the current treeviewitem to the table
                    NodeMap[path] = tw;
                    // Try get the previous state of this node. Copy expanded attribute if found
                    PreviousState.TryGetValue(path, out ITreeElement prev);
                    if (prev != null && prev.Expanded)
                        tw.IsExpanded = true;
                    PreviousState[path] = xNode;
                    Act();
                }
            }
        }

        private void Selected_Changed(object source, RoutedEventArgs e)
        {
            if (View.Props != null)
            {
                View.Props.Update();
            }
        }

        private void TreeViewItem_Expanded(object source, RoutedEventArgs e)
        {
            TreeViewItem el = source as TreeViewItem;
            ((ITreeElement)el.DataContext).ExpandNode(0);
            Act();
        }

        private void TreeViewItem_MouseRightButton(object source, RoutedEventArgs e)
        {
            if (!e.Handled)
            {
                TreeViewItem el = source as TreeViewItem;
                ITreeElement tree_el = el?.DataContext as ITreeElement;
                if (tree_el != null)
                {
                    tree_el.Expanded = true;
                    tree_el.ChainExpand();
                }
                e.Handled = true;
            }
        }

        public void SelectNode(string path)
        {
            if (!NodeMap.ContainsKey(path))
            {
                ActionQueue.Clear();
                ActionQueue.Push(new SelectQueuedAction(path, this));
                ActionQueue.Push(new ExpandQueuedAction(path, this));
                ExpandNodeOnPath(path);
            }
            NodeMap.TryGetValue(path, out TreeViewItem tw);
            if (tw != null)
            {
                EnsureVisible(tw);
                tw.IsSelected = true;
                tw.BringIntoView();
                tw.IsExpanded= true;
            }
        }

        public void Update()
        {
            XmlRoot = View.testCase.TransformedDocument.Root;
            Root = new NodeVisualTreeElement(this.XmlRoot, 0);
            docTreeViewer.ItemsSource = new List<ITreeElement>() { Root };
            docTreeViewer.Items.Refresh();
            docTreeViewer.UpdateLayout();
        }

        public void EnsureVisible(TreeViewItem tw)
        {
            if (!tw.IsExpanded || !tw.IsVisible)
            {
                tw.IsExpanded = true;
                XmlNode node = (tw.DataContext as ITreeElement)?.Node;
                NodeMap.TryGetValue(Utils.getFullPath(node?.ParentNode), out TreeViewItem parent);
                while (parent != null)
                {
                    parent.IsExpanded = true;
                    NodeMap.TryGetValue(Utils.getFullPath((parent.DataContext as ITreeElement)?.Node.ParentNode), out parent);
                }
            }
        }

        public void Act()
        {
            if (ActionQueue.Count > 0)
            {
                ITreeViewQueuedAction action = ActionQueue.Pop();
                if (!action.Act())
                {
                    ActionQueue.Push(action);
                }
            }
        }

        public void ExpandNodeOnPath(string path)
        {
            while (!NodeMap.ContainsKey(path))
            {
                // path is not on NodeMap
                // add it to the Queue
                Console.WriteLine($"Add expand rule for {path}");
                ActionQueue.Push(new ExpandQueuedAction(path, this));
                XmlNode parent = Utils.GetParentNode(Utils.SelectSingleNode(XmlRoot, path));
                if (parent == null)
                {
                    return;
                }
                path = Utils.getFullPath(parent);
            }
            NodeMap[path].IsExpanded = true;
        }
    }
}
