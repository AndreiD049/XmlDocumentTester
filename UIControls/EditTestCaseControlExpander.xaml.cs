using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using XmlTester.Interfaces;
using Ookii.Dialogs.Wpf;
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
    /// Interaction logic for EditTestCaseControlExpander.xaml
    /// </summary>
    public partial class EditTestCaseControlExpander : UserControl
    {
        public TestCaseViewer Page { get; set; }
        public ITestCase TestCase { get; set; }
        public EditTestCaseControlExpander(Control page, ITestCase testCase)
        {
            InitializeComponent();
            Page = (TestCaseViewer)page;
            TestCase = testCase;
            // set checkbox
            Boolean.TryParse(TestCase.GetOption("DefaultExpanded"), out bool expanded);
            DefExpanded.IsChecked = expanded;
            this.DataContext = testCase;
        }

        private void Save_Clicked(object source, RoutedEventArgs e)
        {
            if (newTestCaseName.Text != string.Empty)
            {
                TestCase.Name = newTestCaseName.Text;
                newTestCaseName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            if (newTestCasePath.Text != string.Empty &&
                Directory.Exists(newTestCasePath.Text))
            {
                TestCase.SaveLocation = newTestCasePath.Text;
                newTestCasePath.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            TestCase.AddOption("DefaultExpanded", DefExpanded.IsChecked.ToString());
            TestCase.Document.TestSuiteSaver.SaveSuite();
            Page.newTestCaseExpander.IsExpanded = false;
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

    }
}
