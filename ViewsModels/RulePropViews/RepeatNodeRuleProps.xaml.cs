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
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
{
    /// <summary>
    /// Логика взаимодействия для RepeatNodeRuleProps.xaml
    /// </summary>
    public partial class RepeatNodeRuleProps : GenericRuleProps, ITransformRuleProps  
    {
        public RepeatNodeRuleProps(RepeatNodeTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)rule.Clone(); 
            this.View = View;
            this.View.Props = this;
            this.DataContext = Copy;
        }

        public TextBox xPath 
        {
            get
            {
                return this.Path;
            }
        }
    }
}
