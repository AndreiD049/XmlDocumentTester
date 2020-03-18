using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src.TransformRules
{
    class RemoveNodeTransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode path = node.SelectSingleNode("Path");
            if (path == null)
            {
                Console.Error.WriteLine("No path specified");
                return null;
            }
            return new RemoveNodeTransformRule(path.InnerText);
        }
    }
}
