using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.UIControls;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTesterPresentation
{
    /// <summary>
    /// Interaction logic for TestCaseViewer.xaml
    /// </summary>
    public partial class TestCaseViewer : UserControl 
    {
        public ObservableCollection<ITestCase> TestCases { get; set; }
        public IXMLDocument Document { get; set; }
        public ContentControl ContentArea { get; set; }
        public TestCaseViewer(IXMLDocument doc, ContentControl content)
        {
            InitializeComponent();
            Document = doc;
            TestCases = new ObservableCollection<ITestCase>(doc.TestCases);
            ContentArea = content;
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
            ContentArea.Content = new RuleViewer(testCase, ContentArea);
        }

        private void Expander_Collapsed(object source, RoutedEventArgs e)
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
            testCase.SaveOnLocation();
        }

        private void GenerateAll_Clicked(object source, RoutedEventArgs e)
        {

        }
    }
}
