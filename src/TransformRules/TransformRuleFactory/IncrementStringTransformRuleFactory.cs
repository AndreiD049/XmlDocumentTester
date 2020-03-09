using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src.TransformRules
{
    class IncrementStringTransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode CurrentValue = node.SelectSingleNode("CurrentValue");
            if (CurrentValue == null || CurrentValue.InnerText == string.Empty)
            {
                Console.Error.WriteLine("CurrentValue tag is missing in Increment Rule. Skipping.");
                return null;
            }
            XmlNode path = node.SelectSingleNode("Path");
            if (path == null || path.InnerText == string.Empty)
            {
                Console.Error.WriteLine("Path is missing from Increment Rule. Skipping.");
                return null;
            }
            return new IncrementStringTransformRule(CurrentValue.InnerText, path.InnerText);
        }
    }
}
