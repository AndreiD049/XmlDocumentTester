using System;
using System.Xml;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules.TransformRuleValidators;

namespace XmlTester.src.TransformRules
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
            {
                string fixed_s = FixedString.Trim();
                if (fixed_s != string.Empty)
                {
                    node.InnerText = FixedString;
                }
                else
                {
                    makeNodeEmpty(node);
                }

            }
        }

        private void makeNodeEmpty(XmlNode node)
        {
            XmlDocument doc = node.OwnerDocument;
            // Create a new node with the same name
            XmlElement new_node = doc.CreateElement(node.Name);
            // Copy all the attributes
            foreach(XmlAttribute attr in node.Attributes)
            {
                new_node.SetAttribute(attr.Name, attr.Value);
            }
            if (node.ParentNode.NodeType == XmlNodeType.Document)
            {
                node.InnerText = "";
            }
            else
            {
                // Insert the new Node before the old one
                node.ParentNode.InsertBefore(new_node, node);
                // Remove the old one
                node.ParentNode.RemoveChild(node);
            }
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
