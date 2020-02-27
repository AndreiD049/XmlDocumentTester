using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src;
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
    /// Interaction logic for NewDocControlExpander.xaml
    /// </summary>
    public partial class NewDocControlExpander : UserControl
    {
        public DocPage RelatedPage { get; set; }
        public NewDocControlExpander(Page page)
        {
            InitializeComponent();
            RelatedPage = (DocPage)page;
        }
        private void OpenDlg_Clicked(object source, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                newFilePath.Text = dlg.FileName;
                newFileName.Text = System.IO.Path.GetFileName(dlg.FileName);
            }
        }

        private void CreateDlg_Clicked(object source, RoutedEventArgs e)
        {
            if (newFilePath.Text != string.Empty)
            {
                IXMLDocument doc = new XMLDoc(newFilePath.Text, RelatedPage.App, newFileName.Text);
                Utils.CopyXmlDocWithNewName(doc);
                RelatedPage.SourceDocs.Add(doc);
                RelatedPage.App.AddXmlDoc(doc);
                doc.TestSuiteSaver.SaveSuite();
                newFilePath.Clear();
                newFileName.Clear();
            }
        }

    }
}
