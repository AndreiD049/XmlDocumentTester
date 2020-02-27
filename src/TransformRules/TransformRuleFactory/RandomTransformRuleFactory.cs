using System;
using System.Collections.Generic;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules
{
    class RandomTransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode len = node.SelectSingleNode("Length");
            if (len == null)
            {
                Console.Error.WriteLine("Length tag missing in Random rule. Skipping");
                return null;
            }
            XmlNode allowedChars = node.SelectSingleNode("AllowedChars");
            if (allowedChars == null)
            {
                Console.Error.WriteLine("AllowedChars tag missing in Random rule. Skipping");
                return null;
            }
            XmlNode prefix = node.SelectSingleNode("Prefix");
            if (prefix == null)
            {
                Console.Error.WriteLine("Prefix tag missing in Random rule. Skipping");
                return null;
            }

            XmlNode path = node.SelectSingleNode("Path");
            if (path == null)
            {
                Console.Error.WriteLine($"No path specified");
                return null;
            }
            if (Int32.TryParse(len.InnerText, out int length))
                return new RandomStringTransformRule(length, path.InnerText, allowedChars.InnerText, prefix.InnerText);
            else
                return null;
        }
    }
}
