using System;
using System.Collections.Generic;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;

namespace XmlTesterPresentation.src
{
    class TransformRuleParser
    {
        public static void Parse(XmlNode ruleNode, ITestCase TestCase)
        {
            XmlNode path = ruleNode.SelectSingleNode("Path");
            if (path == null)
            {
                Console.Error.WriteLine("Path tag missing. Skipping");
                return;
            }
            XmlNode type = ruleNode.SelectSingleNode("Type");
            if (type == null)
            {
                Console.Error.WriteLine($"Xml Rule has no type. Skipping.");
                return;
            }
            TransformRuleTypes t;
            if (!Enum.TryParse<TransformRuleTypes>(type.InnerText, true, out t))
            {
                Console.Error.WriteLine($"Not a valid type - {type.InnerText}. Skipping.");
                return;
            }
            // TODO: Create a Transform Rule Factory here, based on the type
            ITransformRuleFactory factory = new TransformRuleFactory();
            IXMLTransformRule rule = factory.createRule(t, ruleNode);
            if (rule != null)
            {
                Console.WriteLine($"adding rule for path {path.InnerText}");
                TestCase.AddRule(path.InnerText, rule);
            }
        }
    }
}
