using System;
using System.Collections.Generic;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules.TransformRuleValidators
{
    class OnlyElementsValidator : ITransformRuleValidator
    {
        public bool Validate(XmlNode node)
        {
            return node.NodeType == XmlNodeType.Element;
        }
    }
}
