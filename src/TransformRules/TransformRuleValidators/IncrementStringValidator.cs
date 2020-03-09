using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using XmlTester.Interfaces;
using XmlTester.src;

namespace XmlTester.src.TransformRules.TransformRuleValidators
{
    class IncrementStringValidator : ITransformRuleValidator
    {
        public IncrementStringTransformRule Rule { get; set; }
        public static readonly Regex _regex = new Regex(@"\d+$", RegexOptions.Compiled);
        public IncrementStringValidator(IncrementStringTransformRule rule)
        {
            Rule = rule;
        }
        public bool Validate(XmlNode node = null)
        {
            return _regex.IsMatch(Rule.CurrentValue);
        }
    }
}
