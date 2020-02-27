using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.src.TransformRules;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FixedStringRuleProps: UserControl, ITransformRuleProps
    {
        public IXMLTransformRule Copy { get; set; }
        public FixedStringTransformRule Rule { get; set; }
        public FixedStringRuleProps(FixedStringTransformRule rule)
        {
            InitializeComponent();
            Rule = rule;
            Copy = new FixedStringTransformRule(rule.FixedString, rule.Path);
            this.DataContext = Copy;
        }
    }
}
