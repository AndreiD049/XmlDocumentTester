using System;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules.TransformRuleValidators;
using System.Collections.Generic;

namespace XmlTesterPresentation.src.TransformRules
{
    public class SequenceTransformRule : IXMLTransformRule, IXmlWriter
    {
        private int nextValue;

        public ITestCase Parent { get; set; }
        public ITransformRuleValidator Validator { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.SequenceString;
        public string Path { get; set; }
        public List<string> Values { get; set; }
        public int NextValue
        {
            get
            {
                if (nextValue >= Values.Count)
                    nextValue = 0;
                return nextValue;
            }
            set
            {
                nextValue = Math.Abs(value % Values.Count);
            }
        }
        public string NextValueItem
        {
            get
            {
                if (Values.Count > 0)
                    return Values[NextValue];
                else
                    return string.Empty;
            }
        }
        // set to true if the value already was transformed
        public bool transformed { get; set; } = false;

        public SequenceTransformRule(List<string> values, string path, int next, bool transformed = false)
        {
            Path = path;
            Values = values;
            NextValue = next;
            this.transformed = transformed;
            Validator = new GenericValidator();
        }

        public object Clone()
        {
            return new SequenceTransformRule(new List<string>(Values), Path, NextValue, transformed);
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
            {
                if (transformed)
                {
                    node.InnerText = Values[NextValue];
                }
                else
                {
                    // Set the node content and increment NextValue
                    node.InnerText = Values[NextValue];
                    NextValue = NextValue + 1;
                    transformed = true;
                }
            }
        }

        private bool TestIdxBounds(int idx)
        {
            return idx >= 0 && idx < Values.Count;
        }
        
        public void Move(int from_idx, int to_idx)
        {
            if (TestIdxBounds(from_idx) && TestIdxBounds(to_idx))
            {
                string tmp = Values[from_idx];
                Values[from_idx] = Values[to_idx];
                Values[to_idx] = tmp;
            }
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("Values", String.Join("<~>", Values.ToArray()));
            writer.WriteElementString("NextValue", NextValue.ToString());
            transformed = false;
        }
    }
}
