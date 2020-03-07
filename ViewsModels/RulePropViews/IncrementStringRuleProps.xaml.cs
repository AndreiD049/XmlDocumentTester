﻿using System.Windows.Controls;
using System.Windows;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
{
    /// <summary>
    /// Interaction logic for IncrementStringRuleProps.xaml
    /// </summary>
    public partial class IncrementStringRuleProps : GenericRuleProps, ITransformRuleProps 
    {
        public IncrementStringRuleProps(IncrementStringTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)Rule.Clone();
            this.View = View;
            this.View.Props = this;
            this.DataContext = Copy;
        }
        public void Update()
        {
            TreeView tree = View.docTreeViewControl;
            NodeTreeViewItem selected_item = tree.SelectedItem as NodeTreeViewItem;
            this.Path.Text = selected_item.FullPath;
            this.CurVal.Text = selected_item.Node.InnerText;
        }
        public new void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Path.Text = "";
            base.Duplicate_Clicked(sender, e);
        }
    }
}