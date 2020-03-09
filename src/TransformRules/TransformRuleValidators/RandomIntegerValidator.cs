using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src.TransformRules.TransformRuleValidators
{
    class RandomIntegerValidator : ITransformRuleValidator
    {
        public RandomIntegerTransformRule Rule { get; set; }
        public RandomIntegerValidator(RandomIntegerTransformRule rule)
        {
            Rule = rule;
        }
        public bool Validate(XmlNode node=null)
        {
            if (Rule.Min > Rule.Max)
                return false;
            return true;
        }
    }
}
