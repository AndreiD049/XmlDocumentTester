using System;
using System.Xml;
using XMLDocumentGenerator.Interfaces;

namespace XMLDocumentGenerator.src.TransformRules
{
    class FixedStringTransformRule : IXMLTransformRule
    {
        public string FixedString { get; set; }

        public FixedStringTransformRule(string fixedStr)
        {
            FixedString = fixedStr;
        }

        public void transform(XmlNode node)
        {
            node.InnerText = FixedString;
        }
    }
}
