using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTester.UIControls.Navigation
{
    /// <summary>
    /// Interaction logic for NavToolbar.xaml
    /// </summary>
    public partial class NavToolbar : UserControl
    {
        public NavigationModel NavModel { get; set; }
        public NavToolbar()
        {
            InitializeComponent();
        }

        public void Set_NavModel(NavigationModel model)
        {
            NavModel = model;
            this.DataContext = NavModel;
        }

        private void Documents_Clicked(object source, RoutedEventArgs e)
        {
            NavModel.GoTo_Documents();
        }

        private void TestCases_Clicked(object source, RoutedEventArgs e)
        {
            NavModel.GoTo_TestCases();
        }
        private void Search_Changed(object source, RoutedEventArgs e)
        {
            NavModel.OnSearchText(SearchBar.Text);
        }
    }
}
