﻿using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;
using System.Windows;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
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
                NodeTreeViewItem selected_item = tree.SelectedItem as NodeTreeViewItem;
                this.Path.Text = selected_item.FullPath;
            }
        }
        public new void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Path.Text = "";
            base.Duplicate_Clicked(sender, e);
        }
    }
}
