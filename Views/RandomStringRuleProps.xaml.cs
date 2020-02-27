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

namespace XmlTesterPresentation.Views
{
    /// <summary>
    /// Interaction logic for RandomStringRuleProps.xaml
    /// </summary>
    public partial class RandomStringRuleProps : UserControl, ITransformRuleProps
    {
        public RandomStringTransformRule Rule { get; set; }
        public IXMLTransformRule Copy { get; set; }

        public RandomStringRuleProps(RandomStringTransformRule rule)
        {
            InitializeComponent();
            Rule = rule;
            Copy = new RandomStringTransformRule(rule.Len, rule.Path, rule.AllowedChars, rule.Prefix);
            this.DataContext = Copy;
        }
    }
}
