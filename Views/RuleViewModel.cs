using System;
using System.Collections.Generic;
using XmlTesterPresentation.Interfaces;
using System.Windows.Controls;
using System.Xml;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace XmlTesterPresentation.Views
{
    public class RuleViewModel
    {
        public RuleViewer Page;
        public ITestCase testCase { get; set; }
        public ObservableCollection<IXMLTransformRule> Rules { get; set; }
        //public Dictionary<string, IXMLTransformRule> rules { get; set; } 
        public RuleViewModel(RuleViewer viewer, ITestCase testCase)
        {
            this.Page = viewer;
            this.testCase = testCase;
            this.Rules = new ObservableCollection<IXMLTransformRule>(this.testCase.rules.Values);
            Init();
            ConstructTree<TreeView>(viewer.ruleTree.docTreeViewer, viewer.testCase.Document.Root);
        }
        private StackPanel _currentSelected;
        public StackPanel currentSelected
        {
            get
            {
                return _currentSelected;
            }
            set
            {
                RemoveButton(_currentSelected);
                _currentSelected = value;
            }
        }

        private void Init()
        {
            Page.ruleList.ruleViewer.ItemsSource = Rules;
        }

        public void DrawProps(IXMLTransformRule rule)
        {
            RulePropsDrawer.DrawRule(Page.ruleProps, rule);
        }

        private void ConstructTree<T>(T parrent, XmlNode currentNode) where T : ItemsControl
        {
            NodeTreeViewItem tw = new NodeTreeViewItem(currentNode);
            tw.Selected += AddButton;
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            Label lbl = new Label() { Content = currentNode.Name};
            

            stack.Children.Add(lbl);

            tw.Header = stack;
            tw.IsExpanded = true;
            parrent.Items.Add(tw);
            foreach (XmlNode node in currentNode.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    ConstructTree<NodeTreeViewItem>(tw, node);
                }
            }
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (!e.Handled)
            {
                StackPanel stack = (StackPanel)((TreeViewItem)sender).Header;
                Button btn = new Button() { Content = " + ", Width = 20, Height = 20 };
                btn.HorizontalContentAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Name = "AddNew";
                stack.Children.Add(btn);
                currentSelected = stack;
                e.Handled = true;
            }
        }

        private void RemoveButton(StackPanel stack)
        {
            if (stack != null)
            {
                foreach (UIElement el in stack.Children)
                {
                    if (el is Button && ((Button)el).Name == "AddNew")
                    {
                        stack.Children.Remove(el);
                        break;
                    }
                }
            }
        }
    }
}
