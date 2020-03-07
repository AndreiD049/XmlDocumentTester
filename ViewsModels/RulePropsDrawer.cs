using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using XmlTesterPresentation;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;
using XmlTesterPresentation.ViewsModels.RulePropViews;

namespace XmlTesterPresentation.ViewsModels
{
    static class RulePropsDrawer
    {
        public static void DrawRule(StackPanel panel, IXMLTransformRule rule, RuleViewer View)
        {
            switch (rule.RuleType)
            {
                case TransformRuleTypes.Fixed:
                    DrawFixedRule(panel, (FixedStringTransformRule)rule, View);
                    break;
                case TransformRuleTypes.Random:
                    DrawRandomRule(panel, (RandomStringTransformRule)rule, View);
                    break;
                case TransformRuleTypes.RandomInteger:
                    DrawRandomIntRule(panel, (RandomIntegerTransformRule)rule, View);
                    break;
                case TransformRuleTypes.RepeatNode:
                    DrawRepeatNodeRule(panel, (RepeatNodeTransformRule)rule, View);
                    break;
                default:
                    break;
            }
        }

        private static void DrawFixedRule(StackPanel panel, FixedStringTransformRule rule, RuleViewer View)
        {
            FixedStringRuleProps drawer = new FixedStringRuleProps(rule, View);
            panel.Children.Clear();
            panel.Children.Add(drawer);
        }

        private static void DrawRandomRule(StackPanel panel, RandomStringTransformRule rule, RuleViewer View)
        {
            RandomStringRuleProps drawer = new RandomStringRuleProps(rule, View);
            panel.Children.Clear();
            panel.Children.Add(drawer);
        }

        private static void DrawRandomIntRule(StackPanel panel, RandomIntegerTransformRule rule, RuleViewer View)
        {
            RandomIntegerRuleProps drawer = new RandomIntegerRuleProps(rule, View);
            panel.Children.Clear();
            panel.Children.Add(drawer);
        }
        private static void DrawRepeatNodeRule(StackPanel panel, RepeatNodeTransformRule rule, RuleViewer View)
        {
            RepeatNodeRuleProps drawer = new RepeatNodeRuleProps(rule, View);
            panel.Children.Clear();
            panel.Children.Add(drawer);
        }
    }
}
