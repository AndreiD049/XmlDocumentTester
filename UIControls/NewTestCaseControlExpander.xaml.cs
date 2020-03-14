using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.Interfaces;
using Ookii.Dialogs.Wpf;
using XmlTester.src;
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
    /// Interaction logic for NewTestCaseControlExpander.xaml
    /// </summary>
    public partial class NewTestCaseControlExpander : UserControl
    {
        public IXMLDocument Document { get; set; }
        public TestCaseViewer Page { get; set; }
        public NewTestCaseControlExpander(Control page, IXMLDocument doc)
        {
            InitializeComponent();
            Document = doc;
            Page = (TestCaseViewer)page;
        }

        private void OpenDlg_Clicked(object source, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderDlg = new VistaFolderBrowserDialog();

            Nullable<bool> result = folderDlg.ShowDialog();

            if (result == true)
            {
                newTestCasePath.Text = folderDlg.SelectedPath;
            }
        }

        private void SaveBtn_Clicked(object source, RoutedEventArgs e)
        {
            if (newTestCaseName.Text != string.Empty && 
                newTestCasePath.Text != string.Empty)
            {
                ITestCase testCase = new TestCase(newTestCaseName.Text, Document, newTestCasePath.Text);
                testCase.AddOption("DefaultExpanded", DefExpanded.IsChecked.ToString());
                Page.TestCases.Add(testCase);
                Document.AddTestCase(testCase);
            }
        }
    }
}
