using System;
using System.Collections.Generic;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;

namespace XmlTesterPresentation.src.TransformRules
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
                case TransformRuleTypes.RepeatNode:
                    return new RepeatNodeTransformRuleFactory().createRule(t, node);
                default:
                    throw new Exception("Not valid Transform rule!");
            }
        }
    }
}
