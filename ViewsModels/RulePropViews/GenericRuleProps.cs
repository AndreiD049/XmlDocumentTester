﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using XmlTester.Interfaces;

namespace XmlTester.ViewsModels.RulePropViews
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
            ITreeElement sel_element = View.ruleTree.docTreeViewer.SelectedItem as ITreeElement;
            XmlNode node = sel_element?.Node; // TODO change 
            if (node != null)
            {
                if (!Copy.Validator.Validate(node))
                {
                    MessageBox.Show("This rule is not valid for the selected Node. Please select other node/rule.", "Invalid Node/Rule", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    View.ViewModel.CloseProps();
                    return;
                }
                View.ViewModel.testCase.AddRule(Copy.Path, Copy);
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
            }
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
                View.ViewModel.RedrawTree();
                View.testCase.Document.TestSuiteSaver.SaveSuite();
            }
        }

        private void Duplicate()
        {
            Rule = (IXMLTransformRule)Copy.Clone();
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
