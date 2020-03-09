using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XmlTester.Interfaces;
using XmlTester.src;
using XmlTester.UIControls;
using XmlTester.UIControls.Navigation;

namespace XmlTester
{
    /// <summary>
    /// Interaction logic for DocPage.xaml
    /// </summary>
    public partial class DocPage : UserControl, ISearchable
    {
        public ObservableCollection<IXMLDocument> SourceDocs { get; set; }
        public IApplication App { get; set; }
        public MainWindow MainWin { get; set; }
        public DocPage(MainWindow mainWin)
        {
            InitializeComponent();
            MainWin = mainWin;
            this.App = mainWin.App;
            SourceDocs = new ObservableCollection<IXMLDocument>(mainWin.App.XmlDocuments);
            Init();

        }

        private void Init()
        {
            docViewer.ItemsSource = SourceDocs;
        }

        private void Doc_Clicked(object source, RoutedEventArgs e)
        {
            Button btn = (Button)source;
            IXMLDocument doc = btn.DataContext as IXMLDocument;
            MainWin.NavModel.GoTo_TestCases(doc);
        }

        private void NewFile_Collapsed(object source, RoutedEventArgs e)
        {
            newFileExpander.IsEnabled = false;
        }

        private void DeleteDoc_Clicked(object source, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Are you sure?", "Delete Document", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button btn = (Button)source;
                IXMLDocument doc = btn.DataContext as IXMLDocument;
                App.RemoveXmlDoc(doc);
                SourceDocs.Remove(doc);
            }
        }

        private void NewDoc_Clicked(object source, RoutedEventArgs e)
        {
            newFileExpander.IsExpanded = true;
            newFileExpander.IsEnabled = true;
            newFileExpander.Content = new NewDocControlExpander(this);
        }

        private void EditDoc_Clicked(object source, RoutedEventArgs e)
        {
            Button btn = (Button)source;
            IXMLDocument doc = btn.DataContext as IXMLDocument;
            newFileExpander.IsExpanded = true;
            newFileExpander.IsEnabled = true;
            newFileExpander.Content = new EditControlExpander(this, doc);
        }

        public void Search(string value)
        {
            string l_value = value.ToLower();
            foreach(Grid g in Utils.FindVisualChildren<Grid>(docViewer))
            {
                foreach(TextBlock t in Utils.FindVisualChildren<TextBlock>(g))
                {
                    string textblock_lower = t.Text.ToLower();
                    if ((string)t.Tag == "Search")
                    {
                        if (textblock_lower.IndexOf(l_value) < 0)
                        {
                            g.Visibility = Visibility.Collapsed;
                        }
                        if (l_value == string.Empty || textblock_lower.IndexOf(l_value) >= 0)
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
