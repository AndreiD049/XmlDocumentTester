using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using XmlTester.Interfaces;


namespace XmlTester.src.TransformRules
{
    class SequenceStringTransformRuleFactory : ITransformRuleFactory
    {
        public IXMLTransformRule createRule(TransformRuleTypes t, XmlNode node)
        {
            XmlNode NextValue = node.SelectSingleNode("NextValue");
            if (NextValue == null)
            {
                Console.Error.WriteLine("NextValue node missing from SequenceString rule. Skipping.");
                return null;
            }
            XmlNode path = node.SelectSingleNode("Path");
            if (path == null)
            {
                Console.Error.WriteLine("Path node missing from SequenceString rule. Skipping.");
                return null;
            }
            XmlNode Values = node.SelectSingleNode("Values");
            if (Values == null)
            {
                Console.Error.WriteLine("Values node missing from SequenceString rule. Skipping.");
                return null;
            }
            if (Int32.TryParse(NextValue.InnerText, out int next_int))
                return new SequenceTransformRule(new List<string>(Values.InnerText.Split("<~>")), path.InnerText, next_int);
            else
                return null;
        }
    }
}
