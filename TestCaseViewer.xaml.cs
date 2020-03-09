﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.Interfaces;
using XmlTester.UIControls;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using XmlTester.src;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTester
{
    /// <summary>
    /// Interaction logic for TestCaseViewer.xaml
    /// </summary>
    public partial class TestCaseViewer : UserControl, ISearchable
    {
        public ObservableCollection<ITestCase> TestCases { get; set; }
        public MainWindow MainWin { get; set; }
        public IXMLDocument Document { get; set; }
        public TestCaseViewer(IXMLDocument doc, MainWindow mainWin)
        {
            InitializeComponent();
            Document = doc;
            MainWin = mainWin;
            TestCases = new ObservableCollection<ITestCase>(doc.TestCases);
            Init();
        }

        private void Init()
        {
            caseViewer.ItemsSource = TestCases;
        }

        private void Test_Clicked(object source, RoutedEventArgs e)
        {
            Button btn = (Button)source;
            ITestCase testCase = btn.DataContext as ITestCase;
            MainWin.NavModel.GoTo_Rules(testCase);
        }

        public void Expander_Collapsed(object source, RoutedEventArgs e)
        {
            newTestCaseExpander.IsEnabled = false;
        }

        private void Delete_Clicked(object source, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete Document", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button btn = (Button)source;
                ITestCase doc = btn.DataContext as ITestCase;
                Document.RemoveTestCase(doc);
                TestCases.Remove(doc);
            }
        }
        private void AddNew_Clicked(object source, RoutedEventArgs e)
        {
            newTestCaseExpander.IsExpanded = true;
            newTestCaseExpander.IsEnabled = true;
            newTestCaseExpander.Content = new NewTestCaseControlExpander(this, Document);
        }

        private void Edit_Clicked(object source, RoutedEventArgs e)
        {
            Button btn = (Button)source;
            ITestCase testCase = btn.DataContext as ITestCase;
            newTestCaseExpander.IsExpanded = true;
            newTestCaseExpander.IsEnabled = true;
            newTestCaseExpander.Content = new EditTestCaseControlExpander(this, testCase);
        }

        private void Generate_Clicked(object source, RoutedEventArgs e)
        {
            Button btn = (Button)source;
            ITestCase testCase = btn.DataContext as ITestCase;
            testCase.generate();
            testCase.UpdateSequentialRules();
            testCase.Document.TestSuiteSaver.SaveSuite();
            testCase.SaveOnLocation();
        }

        private void GenerateAll_Clicked(object source, RoutedEventArgs e)
        {
            foreach(ITestCase tc in TestCases)
            {
                tc.generate();
                tc.UpdateSequentialRules();
                tc.Document.TestSuiteSaver.SaveSuite();
                tc.SaveOnLocation();
            }
        }
        public void Search(string value)
        {
            foreach(Grid g in Utils.FindVisualChildren<Grid>(caseViewer))
            {
                foreach(TextBlock t in Utils.FindVisualChildren<TextBlock>(g))
                {
                    if ((string)t.Tag == "Search")
                    {
                        if (t.Text.IndexOf(value) < 0)
                        {
                            g.Visibility = Visibility.Collapsed;
                        }
                        if (value == string.Empty || t.Text.IndexOf(value) >= 0)
                        {
                            g.Visibility = Visibility.Visible;
                            break;
                        }
                    }
                }
            }
        }
    }
}
