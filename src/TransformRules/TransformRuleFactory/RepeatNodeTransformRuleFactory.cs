using System;
using System.Collections.Generic;
using System.Text;
using XmlTester.Interfaces;
using System.Xml;

namespace XmlTester.src.TransformRules
{
    class RepeatNodeTransformRuleFactory: ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode path = node.SelectSingleNode("Path");
            XmlNode times = node.SelectSingleNode("Times");
            if (path == null)
            {
                Console.Error.WriteLine($"No path specified");
                return null;
            }
            if (times == null)
            {
                Console.Error.WriteLine($"No Times tag specified.");
            }
            if (Int32.TryParse(times.InnerText, out int t_repeat))
                return new RepeatNodeTransformRule(t_repeat, path.InnerText);
            else
                return null;
        }
    }
}
