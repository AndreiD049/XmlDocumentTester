using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.Interfaces;
using XmlTester.ViewsModels;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTester.UIControls
{
    /// <summary>
    /// Interaction logic for RuleListControl.xaml
    /// </summary>
    public partial class RuleListControl : UserControl
    {
        public RuleViewModel ViewModel { get; set; }
        public RuleListControl()
        {
            InitializeComponent();
        }

        private void UpdateRuleProps(object source, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)source;
            IXMLTransformRule rule = item.DataContext as IXMLTransformRule;
            ViewModel.DrawProps(rule);
        }

        private void SelectTreeViewItem(object source, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)source;
            IXMLTransformRule rule = item.DataContext as IXMLTransformRule;
            TreeViewItem tree_node;
            ViewModel.TreeNodesMap.TryGetValue(rule.Path, out tree_node);
            if (tree_node != null)
            {
                tree_node.IsSelected = true;
                tree_node.BringIntoView();
            }
        }
    }
}
