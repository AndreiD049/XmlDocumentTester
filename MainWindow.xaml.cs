using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using XmlTesterPresentation;
using XmlTesterPresentation.src;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;

namespace XmlTesterPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IApplication App { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            IApplication app = new XMLApplication(@"C:\Users\Андрей\source\repos\XmlTesterPresentation\data\");
            XmlDocLoader.LoadXmlDocuments(app);
            App = app;
            _mainFrame.Navigate(new DocPage(app));
        }
    }
}
