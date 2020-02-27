using System;
using System.Collections.Generic;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules
{
    class FixedTransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode fixedString = node.SelectSingleNode("FixedString");
            XmlNode path = node.SelectSingleNode("Path");
            if (fixedString == null)
            {
                Console.Error.WriteLine($"No fixed string in Fixed rule for path");
                return null;
            }
            if (path == null)
            {
                Console.Error.WriteLine($"No path specified");
                return null;
            }
            return new FixedStringTransformRule(fixedString.InnerText, path.InnerText);
        }
    }
}
