using System.Windows;
using System;
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
            SetExceptionHandler();
            IApplication app = new XMLApplication(@"C:\Users\Андрей\source\repos\XmlDocumentTester\data\");
            XmlDocLoader.LoadXmlDocuments(app);
            NavModel = new NavigationModel(this);
            NavigationToolbar.Set_NavModel(NavModel);
            App = app;
            NavModel.GoTo_Documents();
        }

        private void SetExceptionHandler()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
        }
        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = args.ExceptionObject as Exception;
            MessageBox.Show($"Unhandled Exception thrown.\nMessage: {e.Message}\nStack trace: {e.StackTrace}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
