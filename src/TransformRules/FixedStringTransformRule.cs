using System;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules
{
    public class FixedStringTransformRule : IXMLTransformRule, IXmlWriter
    {
        public string FixedString { get; set; }
        public string Path { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.Fixed;

        public FixedStringTransformRule(string fixedStr, string path)
        {
            FixedString = fixedStr;
            Path = path;
        }

        public void transform(XmlNode node)
        {
            node.InnerText = FixedString;
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("FixedString", FixedString);
        }
    }
}
