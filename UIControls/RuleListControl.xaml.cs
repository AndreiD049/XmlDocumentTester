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
            try
            {
                ListViewItem item = (ListViewItem)source;
                IXMLTransformRule rule = item.DataContext as IXMLTransformRule;
                ViewModel.DrawProps(rule);
            }
            catch (Exception err)
            {
                MessageBox.Show($"Unexpected error occured, sorry: {err.Message}\n\n{err.StackTrace}");
            }
        }

        private void SelectTreeViewItem(object source, RoutedEventArgs e)
        {
            try
            {
                ListViewItem item = (ListViewItem)source;
                IXMLTransformRule rule = item.DataContext as IXMLTransformRule;
                ViewModel.View.ruleTree.SelectNode(rule.Path);
            }
            catch (Exception err)
            {
                MessageBox.Show($"Unexpected error occured: {err.Message}\n\n{err.StackTrace}");
            }
}
    }
}
