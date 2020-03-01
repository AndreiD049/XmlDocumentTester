using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.ViewsModels;
using XmlTesterPresentation.src.TransformRules;
using XmlTesterPresentation.src;
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
    /// Interaction logic for NewRulesButtonsList.xaml
    /// </summary>
    public partial class NewRulesButtonsList : UserControl
    {
        public RuleViewer View { get; set; }
        public RuleViewModel ViewModel { get; set; }
        public NewRulesButtonsList()
        {
            InitializeComponent();
        }

        private void CollapseExpander()
        {
            View.newRulesList.IsExpanded = false;
            View.newRulesList.IsEnabled = false;
        }

        private void NewFixed_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath(((NodeTreeViewItem)View.ruleTree.docTreeViewer.SelectedItem)?.Node);
            FixedStringTransformRule rule = new FixedStringTransformRule("", path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }

        private void NewRandom_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath(((NodeTreeViewItem)View.ruleTree.docTreeViewer.SelectedItem)?.Node);
            RandomStringTransformRule rule = new RandomStringTransformRule(0, path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }

        private void NewRepeat_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath(((NodeTreeViewItem)View.ruleTree.docTreeViewer.SelectedItem)?.Node);
            RepeatNodeTransformRule rule = new RepeatNodeTransformRule(1, path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }
    }
}
