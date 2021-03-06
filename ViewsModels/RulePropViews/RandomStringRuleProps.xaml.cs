﻿using System.Windows.Controls;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules;
using System.Windows;
using XmlTester.src;

namespace XmlTester.ViewsModels.RulePropViews
{
    /// <summary>
    /// Interaction logic for RandomStringRuleProps.xaml
    /// </summary>
    public partial class RandomStringRuleProps : GenericRuleProps, ITransformRuleProps 
    {
        public RandomStringRuleProps(RandomStringTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)rule.Clone();
            this.View = View;
            this.View.Props = this;
            this.DataContext = Copy;
        }
        public void Update()
        {
            TreeView tree = View.docTreeViewControl;
            if (tree.SelectedItem != null)
            {
                ITreeElement selected_item = tree.SelectedItem as ITreeElement;
                this.Path.Text = Utils.getFullPath(selected_item.Node);
            }
        }
        public new void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Path.Text = "";
            base.Duplicate_Clicked(sender, e);
        }
    }
}
