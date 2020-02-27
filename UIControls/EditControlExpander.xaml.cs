using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
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
    /// Interaction logic for EditControlExpander.xaml
    /// </summary>
    public partial class EditControlExpander : UserControl
    {
        public IXMLDocument Document { get; set; }
        public DocPage Page { get; set; }
        public EditControlExpander(Page page, IXMLDocument doc)
        {
            InitializeComponent();
            Document = doc;
            Page = (DocPage)page;
            this.DataContext = doc;
        }

        private void EditDlg_Clicked(object source, RoutedEventArgs e)
        {
            Document.TestSuiteSaver.SaveSuite();
        }
    }
}
