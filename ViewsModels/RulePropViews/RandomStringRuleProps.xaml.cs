using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.src.TransformRules;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
{
    /// <summary>
    /// Interaction logic for RandomStringRuleProps.xaml
    /// </summary>
    public partial class RandomStringRuleProps : GenericRuleProps, ITransformRuleProps 
    {
        public TextBox xPath 
        {
            get
            {
                return this.Path;
            }
        }

        public RandomStringRuleProps(RandomStringTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)rule.Clone(); 
            this.View = View;
            this.View.Props = this;
            this.DataContext = Copy;
        }
    }
}
