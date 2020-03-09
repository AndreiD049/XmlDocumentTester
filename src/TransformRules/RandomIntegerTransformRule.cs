using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules.TransformRuleValidators;

namespace XmlTester.src.TransformRules
{
    public class RandomIntegerTransformRule : IXMLTransformRule, IXmlWriter
    {
        public ITestCase Parent { get; set; }
        public ITransformRuleValidator Validator { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.RandomInteger;
        public string Path { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Divisor { get; set; }
        public static Random rnd = new Random();

        public RandomIntegerTransformRule(int Min, int Max, string Path, int Divisor = 0)
        {
            this.Min = Min;
            this.Max = Max;
            this.Path = Path;
            this.Divisor = Divisor;
            Validator = new RandomIntegerValidator(this);
        }

        public object Clone()
        {
            return new RandomIntegerTransformRule(Min, Max, Path, Divisor);
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
            {
                int n = rnd.Next(Min, Max);
                if (Divisor != 0)
                    n = Utils.closestNumber(n, Divisor);
                node.InnerText = n.ToString();

            }
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("Min", Min.ToString());
            writer.WriteElementString("Max", Max.ToString());
        }
    }
}
