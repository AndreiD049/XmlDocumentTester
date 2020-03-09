﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.ViewsModels;
using XmlTester.UIControls;
using XmlTester.Interfaces;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using XmlTester.src;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTester
{
    /// <summary>
    /// Interaction logic for RuleViewer.xaml
    /// </summary>
    public partial class RuleViewer : UserControl, ISearchable
    {
        public RuleViewModel ViewModel { get; set; }
        public ITestCase testCase { get; set; }
        public TreeView docTreeViewControl { get; set; }
        public MainWindow MainWin { get; set; }
        public ITransformRuleProps Props { get; set; }
        public RuleTreeControl ruleTree { get; set; }
        public RuleViewer(ITestCase testCase, MainWindow mainWin)
        {
            InitializeComponent();
            this.testCase = testCase;
            MainWin = mainWin;
            ruleTree = new RuleTreeControl(this);
            ruleTreeContainer.Content = ruleTree;
            docTreeViewControl = ruleTree.docTreeViewer;
            ViewModel = new RuleViewModel(this, testCase);
            ruleList.ViewModel = ViewModel;
            newRuleButtonList.View = this;
            newRuleButtonList.ViewModel = ViewModel;
        }

        private void AddNew_Clicked(object source, RoutedEventArgs e)
        {
            newRulesList.IsEnabled = true;
            newRulesList.IsExpanded = true;
        }
        private void ListViewScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        public void Search(string value)
        {
            string l_value = value.ToLower();
            foreach(TreeViewItem g in Utils.FindVisualChildren<TreeViewItem>(ruleTree))
            {
                foreach(TextBlock t in Utils.FindVisualChildren<TextBlock>(g))
                {
                    if ((string)t.Tag == "Search")
                    {
                        if (t.Text.ToLower().IndexOf(l_value) < 0)
                        {
                            g.Visibility = Visibility.Collapsed;
                        }
                        if (l_value == string.Empty || t.Text.ToLower().IndexOf(l_value) >= 0)
                        {
                            g.Visibility = Visibility.Visible;
                            if (g.IsSelected)
                                g.BringIntoView();
                            break;
                        }
                    }
                }
            }
        }
    }
}
