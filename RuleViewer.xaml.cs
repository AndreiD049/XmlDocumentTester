using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.ViewsModels;
using XmlTester.UIControls;
using XmlTester.Interfaces;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using XmlTester.src;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTester
{
    /// <summary>
    /// Interaction logic for RuleViewer.xaml
    /// </summary>
    public partial class RuleViewer : UserControl, ISearchable
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
            docTreeViewControl = ruleTree.docTreeViewer;
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
        private void ListViewScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        public void Search(string value)
        {
            foreach(NodeTreeViewItem g in Utils.FindVisualChildren<NodeTreeViewItem>(ruleTree))
            {
                foreach(TextBlock t in Utils.FindVisualChildren<TextBlock>(g))
                {
                    if ((string)t.Tag == "Search")
                    {
                        if (t.Text.IndexOf(value) < 0)
                        {
                            g.Visibility = Visibility.Collapsed;
                        }
                        if (value == string.Empty || t.Text.IndexOf(value) >= 0)
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
