using System;
using System.Collections.Generic;
using XmlTester.Interfaces;
using System.Windows.Controls;
using System.Xml;
using XmlTester.src;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace XmlTester.ViewsModels
{
    public class RuleViewModel
    {
        public RuleViewer View;
        public ITestCase testCase { get; set; }
        public ObservableCollection<IXMLTransformRule> Rules { get; set; }
        public Dictionary<string, TreeViewItem> TreeNodesMap { get; set; }
        public RuleViewModel(RuleViewer viewer, ITestCase testCase)
        {
            this.View = viewer;
            this.testCase = testCase;
            this.Rules = new ObservableCollection<IXMLTransformRule>(Utils.FlattenTransformRules(this.testCase.rules));
            Init();
            testCase.generate();
            TreeNodesMap = new Dictionary<string, TreeViewItem>();
            //ConstructTree<TreeView>(viewer.ruleTree.docTreeViewer, viewer.testCase.TransformedDocument.Root);
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
            TreeViewItem tw = null;
            StackPanel stack = new StackPanel();
            if (currentNode.Attributes != null)
            {
                foreach (XmlNode attr in currentNode.Attributes)
                {
                    TreeViewItem attr_tw = null; 
                    tw.Items.Add(attr_tw);
                }
            }
            foreach (XmlNode node in currentNode.ChildNodes)
            {
                ConstructTree<TreeViewItem>(tw, node);
            }
        }

        private void UpdateTreeNodesMap()
        {
            // Initialize the stack where we'll put the nodes and their childs
            Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
            // add the top level children to the stack
            foreach (TreeViewItem node in View.docTreeViewControl.Items)
                stack.Push(node);
            // add the items and their children to the @TreeNodesMap until the stack is not empty
            while (stack.Count > 0)
            {
                TreeViewItem current_item = stack.Pop();
                // Try add the current node the hash table
                if (current_item != null) // TODO Change
                    TreeNodesMap[""] = current_item;
                foreach (TreeViewItem child in current_item.Items)
                {
                    stack.Push(child);
                }
            }
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
                string selected_item_path = ""; // TODO Change
                ClearTree(View.ruleTree.docTreeViewer);
                ConstructTree<TreeView>(View.ruleTree.docTreeViewer, testCase.TransformedDocument.Root);
                if (View.ruleTree.docTreeViewer.Items.Count > 0)
                {
                    ((TreeViewItem)View.ruleTree.docTreeViewer.Items[0]).Loaded += UpdateSearch;
                    ((TreeViewItem)View.ruleTree.docTreeViewer.Items[0]).Loaded += (object source, RoutedEventArgs e) => { BringSelectedIntoView(selected_item_path); };
                }
                UpdateTreeNodesMap();
            }
        }

        private void BringSelectedIntoView(string selected_item_path)
        {
            if (selected_item_path != null)
            {
                View.ViewModel.TreeNodesMap.TryGetValue(selected_item_path, out TreeViewItem selected_node);
                if (selected_node != null)
                {
                    selected_node.BringIntoView();
                    selected_node.IsSelected = true;
                }
            }
        }

        private void UpdateSearch(object source, RoutedEventArgs e)
        {
            View.Search(View.MainWin.NavigationToolbar.SearchBar.Text);
        }

        public void CloseProps()
        {
            View.ruleProps.Children.Clear();
            View.Props = null;
        }
    }
}
