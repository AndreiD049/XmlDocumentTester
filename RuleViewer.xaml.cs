using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.ViewsModels;
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
    public partial class RuleViewer : UserControl
    {
        public RuleViewModel ViewModel { get; set; }
        public ITestCase testCase { get; set; }
        public TreeView docTreeViewControl { get; set; }
        public MainWindow MainWin { get; set; }
        public ITransformRuleProps Props { get; set; }
        public RuleViewer(ITestCase testCase, MainWindow mainWin)
        {
            InitializeComponent();
            this.testCase = testCase;
            MainWin = mainWin;
            ViewModel = new RuleViewModel(this, testCase);
            docTreeViewControl = ruleTree.docTreeViewer;
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
