using System;
using System.Collections.Generic;
using System.Text;
using XmlTester.src.TransformRules.TransformRuleValidators;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src.TransformRules
{
    public class RemoveNodeTransformRule : IXMLTransformRule, IXmlWriter
    {
        public ITestCase Parent { get; set; }
        public ITransformRuleValidator Validator { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.RemoveNode;
        public string Path { get; set; }

        public RemoveNodeTransformRule(string Path)
        {
            this.Path = Path;
            Validator = new RemoveNodeValidator();
        }

        public object Clone()
        {
            return new RemoveNodeTransformRule(Path);
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
                Parent.NodesToDelete.Add(node);
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
        }
    }
}
