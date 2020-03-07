using System;
using System.Collections.Generic;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.UIControls.DocumentTreeElements;
using System.Windows.Controls;
using System.Xml;
using XmlTesterPresentation.src;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace XmlTesterPresentation.ViewsModels
{
    public class RuleViewModel
    {
        public RuleViewer View;
        public ITestCase testCase { get; set; }
        public ObservableCollection<IXMLTransformRule> Rules { get; set; }
        public Dictionary<string, NodeTreeViewItem> TreeNodesMap { get; set; }
        public RuleViewModel(RuleViewer viewer, ITestCase testCase)
        {
            this.View = viewer;
            this.testCase = testCase;
            this.Rules = new ObservableCollection<IXMLTransformRule>(Utils.FlattenTransformRules(this.testCase.rules));
            Init();
            testCase.generate();
            TreeNodesMap = new Dictionary<string, NodeTreeViewItem>();
            ConstructTree<TreeView>(viewer.ruleTree.docTreeViewer, viewer.testCase.TransformedDocument.Root);
            UpdateTreeNodesMap();
        }

        private void Init()
        {
            View.ruleList.ruleViewer.ItemsSource = Rules;
        }

        public void DrawProps(IXMLTransformRule rule)
        {
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
        }

        private void ConstructTree<T>(T parrent, XmlNode currentNode) where T : ItemsControl
        {
            NodeTreeViewItem tw = new NodeTreeViewItem(currentNode);
            StackPanel stack = new StackPanel();
            if (RenderXmlElement(tw, currentNode))
                parrent.Items.Add(tw);
            if (currentNode.Attributes != null)
            {
                foreach (XmlNode attr in currentNode.Attributes)
                {
                    NodeTreeViewItem attr_tw = new NodeTreeViewItem(attr);
                    tw.Items.Add(attr_tw);
                    RenderXmlElement(attr_tw, attr);
                }
            }
            foreach (XmlNode node in currentNode.ChildNodes)
            {
                ConstructTree<NodeTreeViewItem>(tw, node);
            }
        }

        private void UpdateTreeNodesMap()
        {
            // Initialize the stack where we'll put the nodes and their childs
            Stack<NodeTreeViewItem> stack = new Stack<NodeTreeViewItem>();
            // add the top level children to the stack
            foreach (NodeTreeViewItem node in View.docTreeViewControl.Items)
                stack.Push(node);
            // add the items and their children to the @TreeNodesMap until the stack is not empty
            while (stack.Count > 0)
            {
                NodeTreeViewItem current_item = stack.Pop();
                // Try add the current node the hash table
                if (current_item.FullPath != null)
                    TreeNodesMap[current_item.FullPath] = current_item;
                foreach (NodeTreeViewItem child in current_item.Items)
                {
                    stack.Push(child);
                }
            }
        }

        private bool RenderXmlElement(NodeTreeViewItem tw, XmlNode node)
        {
            Control ctrl;
            switch (node.NodeType)
            {
                case XmlNodeType.Element:
                    ctrl = new ElementNode();
                    ctrl.DataContext = node;
                    tw.Header = ctrl;
                    tw.IsExpanded = true;
                    return true;
                case XmlNodeType.Attribute:
                    ctrl = new AtributeNode();
                    ctrl.DataContext = node;
                    tw.Header = ctrl;
                    tw.IsExpanded = true;
                    tw.IsEnabled = true;
                    return true;
                case XmlNodeType.Text:
                    ctrl = new TextNode();
                    ctrl.DataContext = node;
                    tw.Header = ctrl;
                    tw.IsExpanded = true;
                    tw.IsEnabled = false;
                    tw.Cursor = System.Windows.Input.Cursors.Arrow;
                    return true;
                default:
                    Console.Error.WriteLine("Unknown type of node");
                    break;
            }
            return false;
        }

        private void ClearTree(TreeView parent)
        {
            parent.Items.Clear();
        }

        public void RedrawTree()
        {
            if (testCase != null)
            {
                testCase.generate();
                ClearTree(View.ruleTree.docTreeViewer);
                ConstructTree<TreeView>(View.ruleTree.docTreeViewer, testCase.TransformedDocument.Root);
                UpdateTreeNodesMap();
            }
        }

        public void CloseProps()
        {
            View.ruleProps.Children.Clear();
            View.Props = null;
        }
    }
}
