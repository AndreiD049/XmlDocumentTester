using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src;
using XmlTesterPresentation.UIControls;

namespace XmlTesterPresentation
{
    /// <summary>
    /// Interaction logic for DocPage.xaml
    /// </summary>
    public partial class DocPage : Page
    {
        public ObservableCollection<IXMLDocument> SourceDocs { get; set; }
        public IApplication App { get; set; }
        public DocPage(IApplication app)
        {
            InitializeComponent();
            this.App = app;
            SourceDocs = new ObservableCollection<IXMLDocument>(app.XmlDocuments);
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
            NavigationService.Navigate(new TestCaseViewer(doc));
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
    }
}
