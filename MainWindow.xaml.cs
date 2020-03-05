using System.Windows;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src;
using XmlTesterPresentation.UIControls.Navigation;

namespace XmlTesterPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IApplication App { get; set; }
        public NavigationModel NavModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            IApplication app = new XMLApplication(@"C:\Users\я\source\repos\XMLDocumentGenerator\XMLDocumentGenerator\data\");
            XmlDocLoader.LoadXmlDocuments(app);
            NavModel = new NavigationModel(this);
            NavigationToolbar.Set_NavModel(NavModel);
            App = app;
            NavModel.GoTo_Documents();
        }
    }
}
