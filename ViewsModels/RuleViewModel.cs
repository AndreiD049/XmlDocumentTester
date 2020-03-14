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
        public RuleViewModel(RuleViewer viewer, ITestCase testCase)
        {
            this.View = viewer;
            this.testCase = testCase;
            this.Rules = new ObservableCollection<IXMLTransformRule>(Utils.FlattenTransformRules(this.testCase.rules));
            Init();
        }

        private void Init()
        {
            View.ruleList.ruleViewer.ItemsSource = Rules;
        }

        public void DrawProps(IXMLTransformRule rule)
        {
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
        }

        public void RedrawTree()
        {
            if (testCase != null)
            {
                testCase.generate();
                View.ruleTree.Update();
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
