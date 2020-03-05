using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules.TransformRuleValidators;

namespace XmlTesterPresentation.src.TransformRules
{
    public class RepeatNodeTransformRule : IXMLTransformRule, IXmlWriter
    {
        public RepeatNodeTransformRule(int times, string path)
        {
            Times = times;
            Path = path;
            Validator = new OnlyElementsValidator();
        }
        public ITestCase Parent { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.RepeatNode;
        public string Path { get; set; }

        public int Times { get; set; }
        public ITransformRuleValidator Validator { get; set; }

        public object Clone()
        {
            return new RepeatNodeTransformRule(Times, Path);
        }

        public void transform(XmlNode node)
        {
            if (!Validator.Validate(node))
                return;
            for (int i = 0; i < Times; ++i)
            {
                XmlNode copy = node.Clone();
                node.ParentNode.InsertAfter(copy, node);
                // Transform all the subset nodes
                foreach (XmlNode x in Utils.getAllNodesEnumerable(copy))
                {
                    string fullPath = Utils.getFullPath(x);
                    if (Parent.rules.ContainsKey(fullPath))
                    {
                        foreach (IXMLTransformRule rule in Parent.rules[fullPath])
                            rule.transform(x);
                    }
                }
            }
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("Times", Times.ToString());
        }
    }
}
