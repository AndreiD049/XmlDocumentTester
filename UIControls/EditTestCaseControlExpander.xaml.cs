using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
using Ookii.Dialogs.Wpf;
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
    /// Interaction logic for EditTestCaseControlExpander.xaml
    /// </summary>
    public partial class EditTestCaseControlExpander : UserControl
    {
        public TestCaseViewer Page { get; set; }
        public ITestCase TestCase { get; set; }
        public EditTestCaseControlExpander(Page page, ITestCase testCase)
        {
            InitializeComponent();
            Page = (TestCaseViewer)page;
            TestCase = testCase;
            this.DataContext = testCase;
        }

        private void Save_Clicked(object source, RoutedEventArgs e)
        {
            if (newTestCaseName.Text != string.Empty)
                TestCase.Name = newTestCaseName.Text;
            if (newTestCasePath.Text != string.Empty &&
                Directory.Exists(newTestCasePath.Text))
                TestCase.SaveLocation = newTestCasePath.Text;
            TestCase.Document.TestSuiteSaver.SaveSuite();
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
