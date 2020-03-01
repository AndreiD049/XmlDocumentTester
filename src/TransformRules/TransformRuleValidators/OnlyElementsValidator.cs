using System;
using System.Collections.Generic;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules.TransformRuleValidators
{
    class OnlyElementsValidator : ITransformRuleValidator
    {
        public bool Validate(XmlNode rule)
        {
            return rule.NodeType == XmlNodeType.Element;
        }
    }
}
