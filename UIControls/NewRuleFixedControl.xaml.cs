using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Views;
using XmlTesterPresentation.src;
using XmlTesterPresentation.src.TransformRules;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTesterPresentation.UIControls
{
    /// <summary>
    /// Interaction logic for NewRuleFixedControl.xaml
    /// </summary>
    public partial class NewRuleFixedControl : UserControl
    {
        public RuleViewModel Model { get; set; }
        public NewRuleFixedControl(RuleViewModel model)
        {
            InitializeComponent();
            Model = model;
            Model.Page.ruleTree.docTreeViewer.SelectedItemChanged += OnSelectionChange;
            NodeTreeViewItem node = (NodeTreeViewItem)Model.Page.ruleTree.docTreeViewer.SelectedItem;
            if (node != null)
                newRuleXPath.Text = Utils.getFullPath(node.Node);
        }

        private void OnSelectionChange(object sender, RoutedEventArgs e)
        {
            newRuleXPath.Text = Utils.getFullPath(((NodeTreeViewItem)Model.Page.ruleTree.docTreeViewer.SelectedItem).Node);
        }

        private void SaveRule_Clicked(object sender, RoutedEventArgs e)
        {
            if (newRuleXPath.Text != string.Empty && newFixedString.Text != string.Empty)
            {
                FixedStringTransformRule newRule = new FixedStringTransformRule(newFixedString.Text, newRuleXPath.Text);
                if (Model.Page.testCase.AddRule(newRuleXPath.Text, newRule))
                {
                    Model.Rules.Add(newRule);
                    Model.testCase.Document.TestSuiteSaver.SaveSuite();
                }
                else
                {
                    MessageBox.Show("Rule for this path already exists. Remove it first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // Close itself
            //Model.Page.newRuleView.Children.Clear();
        }
    }
}
