using System;
using System.Collections.Generic;
using System.Xml;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules;

namespace XmlTester.src.TransformRules
{
    class TransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            switch (t)
            {
                case TransformRuleTypes.Fixed:
                    return new FixedTransformRuleFactory().createRule(t, node);
                case TransformRuleTypes.Random:
                    return new RandomTransformRuleFactory().createRule(t, node);
                case TransformRuleTypes.RandomInteger:
                    return new RandomIntegerTransformRuleFactory().createRule(t, node);
                case TransformRuleTypes.RepeatNode:
                    return new RepeatNodeTransformRuleFactory().createRule(t, node);
                case TransformRuleTypes.IncrementString:
                    return new IncrementStringTransformRuleFactory().createRule(t, node);
                case TransformRuleTypes.SequenceString:
                    return new SequenceStringTransformRuleFactory().createRule(t, node);
                case TransformRuleTypes.RemoveNode:
                    return new RemoveNodeTransformRuleFactory().createRule(t, node);
                default:
                    throw new Exception("Not valid Transform rule!");
            }
        }
    }
}
