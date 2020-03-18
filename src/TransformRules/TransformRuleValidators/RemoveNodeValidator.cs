using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src.TransformRules.TransformRuleValidators
{
    class RemoveNodeValidator : ITransformRuleValidator
    {
        public bool Validate(XmlNode node = null)
        {
            if (node != null)
            {
                if (node.NodeType == XmlNodeType.Element)
                    return (node.ParentNode != null || node.ParentNode.NodeType == XmlNodeType.Document);
                else if (node.NodeType == XmlNodeType.Attribute)
                    return ((XmlAttribute)node)?.OwnerElement != null;
            }
            return false;
        }
    }
}
