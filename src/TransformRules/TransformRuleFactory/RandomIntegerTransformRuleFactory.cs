using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules
{
    class RandomIntegerTransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode min = node.SelectSingleNode("Min");
            if (min == null)
            {
                Console.Error.WriteLine("Min tag missing in Random rule. Skipping");
                return null;
            }
            XmlNode max = node.SelectSingleNode("Min");
            if (max == null)
            {
                Console.Error.WriteLine("Max tag missing in Random rule. Skipping");
                return null;
            }
            XmlNode path = node.SelectSingleNode("Path");
            if (path == null)
            {
                Console.Error.WriteLine($"No path specified");
                return null;
            }
            if (Int32.TryParse(min.InnerText, out int min_int) && Int32.TryParse(max.InnerText, out int max_int))
                return new RandomIntegerTransformRule(min_int, max_int, path.InnerText);
            else
                return null;
        }
    }
}
