using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Views;
using XmlTesterPresentation.UIControls;
using XmlTesterPresentation.Interfaces;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTesterPresentation
{
    /// <summary>
    /// Interaction logic for RuleViewer.xaml
    /// </summary>
    public partial class RuleViewer : Page
    {
        private RuleViewModel ViewModel { get; set; }
        public ITestCase testCase { get; set; }
        public RuleViewer(ITestCase testCase)
        {
            InitializeComponent();
            this.testCase = testCase;
            ViewModel = new RuleViewModel(this, testCase);
            ruleList.ViewModel = ViewModel;
            newRuleButtonList.View = this;
            newRuleButtonList.ViewModel = ViewModel;
            ruleTree.View = this;
            ruleTree.ViewModel = ViewModel;
        }
        
        private void AddNew_Clicked(object source, RoutedEventArgs e)
        {
            newRulesList.IsEnabled = true;
            newRulesList.IsExpanded = true;
        }
    }
}
