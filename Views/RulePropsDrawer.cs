using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using XmlTesterPresentation;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;

namespace XmlTesterPresentation.Views
{
    static class RulePropsDrawer
    {
        public static void DrawRule(StackPanel panel, IXMLTransformRule rule)
        {
            switch (rule.RuleType)
            {
                case TransformRuleTypes.Fixed:
                    DrawFixedRule(panel, (FixedStringTransformRule)rule);
                    break;
                case TransformRuleTypes.Random:
                    DrawRandomRule(panel, (RandomStringTransformRule)rule);
                    break;
                default:
                    break;
            }
        }

        private static void DrawFixedRule(StackPanel panel, FixedStringTransformRule rule)
        {
            FixedStringRuleProps drawer = new FixedStringRuleProps(rule);
            panel.Children.Clear();
            panel.Children.Add(drawer);
        }

        private static void DrawRandomRule(StackPanel panel, RandomStringTransformRule rule)
        {
            RandomStringRuleProps drawer = new RandomStringRuleProps(rule);
            panel.Children.Clear();
            panel.Children.Add(drawer);
        }
    }
}
