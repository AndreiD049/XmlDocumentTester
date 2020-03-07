using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules.TransformRuleValidators;

namespace XmlTesterPresentation.src.TransformRules
{
    public class RandomIntegerTransformRule : IXMLTransformRule, IXmlWriter
    {
        public ITestCase Parent { get; set; }
        public ITransformRuleValidator Validator { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.RandomInteger;
        public string Path { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public static Random rnd = new Random();

        public RandomIntegerTransformRule(int Min, int Max, string Path)
        {
            this.Min = Min;
            this.Max = Max;
            this.Path = Path;
            Validator = new GenericValidator();
        }

        public object Clone()
        {
            return new RandomIntegerTransformRule(Min, Max, Path);
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
                node.InnerText = rnd.Next(Min, Max).ToString();
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("Min", Min.ToString());
            writer.WriteElementString("Max", Max.ToString());
        }
    }
}
