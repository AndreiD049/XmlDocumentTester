using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src.TransformRules.TransformRuleValidators
{
    class IncrementStringValidator : ITransformRuleValidator
    {
        public static readonly Regex _regex = new Regex(@"\d+$", RegexOptions.Compiled);
        public bool Validate(XmlNode node = null)
        {
            return _regex.IsMatch(node.InnerText);
        }
    }
}
