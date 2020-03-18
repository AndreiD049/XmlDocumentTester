using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.ViewsModels;
using XmlTester.src.TransformRules;
using XmlTester.src;
using System.Xml;
using XmlTester.Interfaces;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTester.UIControls
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
            string path = Utils.getFullPath((View.ruleTree.docTreeViewer.SelectedItem as ITreeElement)?.Node);
            FixedStringTransformRule rule = new FixedStringTransformRule("", path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }

        private void NewRandom_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath((View.ruleTree.docTreeViewer.SelectedItem as ITreeElement)?.Node);
            RandomStringTransformRule rule = new RandomStringTransformRule(0, path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }

        private void NewRandomInt_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath((View.ruleTree.docTreeViewer.SelectedItem as ITreeElement)?.Node);
            RandomIntegerTransformRule rule = new RandomIntegerTransformRule(0, 100000, path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }

        private void NewRepeat_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath((View.ruleTree.docTreeViewer.SelectedItem as ITreeElement)?.Node);
            RepeatNodeTransformRule rule = new RepeatNodeTransformRule(1, path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }
        private void NewRemove_Clicked(object source, RoutedEventArgs e)
        {
            string path = Utils.getFullPath((View.ruleTree.docTreeViewer.SelectedItem as ITreeElement)?.Node);
            RemoveNodeTransformRule rule = new RemoveNodeTransformRule(path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }
        private void NewIncrement_Clicked(object source, RoutedEventArgs e)
        {
            ITreeElement item = (ITreeElement)View.ruleTree.docTreeViewer.SelectedItem;
            string path = Utils.getFullPath(item?.Node as XmlNode);
            string current_value = "";
            if (item != null)
                current_value = ((XmlNode)item.Node).InnerText;
            IncrementStringTransformRule rule = new IncrementStringTransformRule(current_value, path);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }

        private void NewSequence_Clicked(object source, RoutedEventArgs e)
        {
            ITreeElement item = (ITreeElement)View.ruleTree.docTreeViewer.SelectedItem;
            string path = Utils.getFullPath(item?.Node as XmlNode);
            string current_value = "Value";
            if (item != null)
                current_value = item.Node.InnerText;
            SequenceTransformRule rule = new SequenceTransformRule(new List<string>() { current_value }, path, 0);
            RulePropsDrawer.DrawRule(View.ruleProps, rule, View);
            CollapseExpander();
        }
    }
}
