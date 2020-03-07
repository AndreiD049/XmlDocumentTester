using System;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules.TransformRuleValidators;

namespace XmlTesterPresentation.src.TransformRules
{
    /// <summary>
    /// Transform rule that will increment the current Node Value.
    /// Will work only if the value ends with numeric characters.
    /// Specific fields used:
    ///  * Current Value;
    /// </summary>
    public class IncrementStringTransformRule : IXMLTransformRule, IXmlWriter
    {
        public ITestCase Parent { get; set; }
        public ITransformRuleValidator Validator { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.IncrementString;
        public string Path { get; set; }
        public string CurrentValue { get; set; }
        public string IncrementedValue { get; set; }

        public static readonly Regex _regex = new Regex(@"\d+$", RegexOptions.Compiled);

        public IncrementStringTransformRule(string val, string path)
        {
            CurrentValue = val;
            Path = path;
            Validator = new IncrementStringValidator(); 
        }

        public object Clone()
        {
            return new IncrementStringTransformRule(CurrentValue, Path);
        }

        private string IncrementString(string val)
        {
            MatchCollection matches = _regex.Matches(val);
            if (matches.Count > 0)
            {
                int increment = Int32.Parse(matches[0].Value) + 1;
                // Return the incremented string.
                // If the incremented value is bigger than the initial numeric value, we just append it to the base string.
                // Else, we need to concatenate the intial numeric value, with the incremented string.
                return val.Substring(0, val.Length - matches[0].Value.Length) + 
                       ((matches[0].Value.Length - increment.ToString().Length) < 0 ? 
                         increment.ToString() : 
                         matches[0].Value.Substring(0, matches[0].Value.Length - increment.ToString().Length) + increment.ToString()); 
            }
            return val; // Rewrite; user Matches and Replace to replace the incremented value
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
            {
                if (IncrementedValue != null)
                {
                    node.InnerText = IncrementedValue;
                }
                else
                {
                    node.InnerText = IncrementString(CurrentValue);
                    IncrementedValue = node.InnerText;
                }
            }
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("CurrentValue", CurrentValue);
            if (IncrementedValue != null)
            {
                CurrentValue = IncrementedValue;
                IncrementedValue = null;
            }
        }
    }
}
