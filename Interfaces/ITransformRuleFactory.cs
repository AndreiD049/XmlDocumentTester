using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlTester.Interfaces
{
    public interface ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node);
    }
}
