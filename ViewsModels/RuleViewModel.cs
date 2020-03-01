using System;
using System.Collections.Generic;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.UIControls.DocumentTreeElements;
using System.Windows.Controls;
using System.Xml;
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
        //public Dictionary<string, IXMLTransformRule> rules { get; set; }
        public RuleViewModel(RuleViewer viewer, ITestCase testCase)
        {
            this.View = viewer;
            this.testCase = testCase;
            this.Rules = new ObservableCollection<IXMLTransformRule>(this.testCase.rules.Values);
            Init();
            testCase.generate();
            ConstructTree<TreeView>(viewer.ruleTree.docTreeViewer, viewer.testCase.TransformedDocument.Root);
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
                    TreeViewItem attr_tw = new NodeTreeViewItem(attr);
                    tw.Items.Add(attr_tw);
                    RenderXmlElement(attr_tw, attr);
                }
            }
            foreach (XmlNode node in currentNode.ChildNodes)
            {
                ConstructTree<NodeTreeViewItem>(tw, node);
            }
        }

        private bool RenderXmlElement(TreeViewItem tw, XmlNode node)
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
            }
        }

        public void CloseProps()
        {
            View.ruleProps.Children.Clear();
            View.Props = null;
        }
    }
}
