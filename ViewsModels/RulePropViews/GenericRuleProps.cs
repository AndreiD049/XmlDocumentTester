using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
{
    public class GenericRuleProps : UserControl
    {
        public GenericRuleProps()
        {
        }
        public RuleViewer View { get; set; }
        public IXMLTransformRule Copy { get; set; }
        public IXMLTransformRule Rule { get; set; }
        public void Save()
        {
            // Check if Node is Valid
            XmlNode node = ((NodeTreeViewItem)View.docTreeViewControl.SelectedItem).Node;
            if (!Copy.Validator.Validate(node))
            {
                MessageBox.Show("This rule is not valid for the selected Node. Please select other node/rule.", "Invalid Node/Rule", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                View.ViewModel.CloseProps();
                return;
            }
            // TODO - Delete below comments
            //bool added = 
            View.ViewModel.testCase.AddRule(Copy.Path, Copy);
            //if (!added)
            //{
            //    if (Rule.Parent == null)
            //    {
            //        MessageBox.Show("Rule was not added. A rule with this path already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //    else
            //    {
            //        if (MessageBox.Show("Are you sure to modify?", "Modify Rule", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //        {
            //            Copy.Parent = Rule.Parent;
            //            View.ViewModel.testCase.rules[Rule.Path] = Copy;
            //            int obsIndex = View.ViewModel.Rules.IndexOf(Rule);
            //            View.ViewModel.Rules[obsIndex] = Copy;
            //        }
            //    }
            //}
            //else
            //{
                if (Rule.Parent == null)
                {
                    View.ViewModel.Rules.Add(Copy);
                }
                else
                {
                    if (MessageBox.Show("Are you sure to modify?", "Modify Rule", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        View.ViewModel.testCase.RemoveRule(Rule.Path, Rule);
                        int obsIndex = View.ViewModel.Rules.IndexOf(Rule);
                        View.ViewModel.Rules[obsIndex] = Copy;
                    }
                    else
                    {
                        View.ViewModel.testCase.RemoveRule(Copy.Path, Rule);
                    }
                }
            //}
            View.ViewModel.CloseProps();
            View.testCase.Document.TestSuiteSaver.SaveSuite();
        }

        private void Delete()
        {
            if (MessageBox.Show("Are you sure?", "Delete Rule", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                View.ViewModel.testCase.RemoveRule(Rule.Path, Rule);
                View.ViewModel.Rules.Remove(Rule);
                View.ViewModel.CloseProps();
                View.testCase.Document.TestSuiteSaver.SaveSuite();
            }
        }

        private void Duplicate()
        {
            Rule = (IXMLTransformRule)Copy.Clone();
            View.Props.xPath.Text = "";
        }

        protected void Cancel_Clicked(object source, RoutedEventArgs e)
        {
            View.ViewModel.CloseProps();
        }

        protected void Delete_Clicked(object source, RoutedEventArgs e)
        {
            Delete();
            View.ViewModel.RedrawTree();
        }

        protected void Save_Clicked(object source, RoutedEventArgs e)
        {
            Save();
            View.ViewModel.RedrawTree();
        }

        protected void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Duplicate();
        }
    }
}
