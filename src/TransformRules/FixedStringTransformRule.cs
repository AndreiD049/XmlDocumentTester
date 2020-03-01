using System;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules.TransformRuleValidators;

namespace XmlTesterPresentation.src.TransformRules
{
    public class FixedStringTransformRule : IXMLTransformRule, IXmlWriter
    {
        public ITestCase Parent { get; set; } = null;
        public string FixedString { get; set; }
        public string Path { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.Fixed;
        public ITransformRuleValidator Validator { get; set; }

        public FixedStringTransformRule(string fixedStr, string path)
        {
            FixedString = fixedStr;
            Path = path;
            Validator = new GenericValidator();
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
                node.InnerText = FixedString;
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("FixedString", FixedString);
        }

        public object Clone()
        {
            return new FixedStringTransformRule(FixedString, Path);
        }
    }
}
