using System;
using System.Xml;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules.TransformRuleValidators;
using System.Collections.Generic;

namespace XmlTester.src.TransformRules
{
    public class SequenceTransformRule : IXMLTransformRule, IXmlWriter, ISequentialRule
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

        public SequenceTransformRule(List<string> values, string path, int next)
        {
            Path = path;
            Values = values;
            NextValue = next;
            Validator = new GenericValidator();
        }

        public object Clone()
        {
            return new SequenceTransformRule(new List<string>(Values), Path, NextValue);
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
            {
                node.InnerText = Values[NextValue];
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
        }

        public void UpdateSequencialValues()
        {
            NextValue += 1;
        }
    }
}
