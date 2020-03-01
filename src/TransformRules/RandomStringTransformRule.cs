using System;
using System.Xml;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules.TransformRuleValidators;

namespace XmlTesterPresentation.src.TransformRules
{
    public class RandomStringTransformRule : IXMLTransformRule, IXmlWriter
    {
        public ITestCase Parent { get; set; } = null;
        public int Len { get; set; }
        public TransformRuleTypes RuleType { get; set; } = TransformRuleTypes.Random;
        public string Path { get; set; }
        public XmlNode Node { get; }
        public string AllowedChars { get; set; }
        public string Prefix { get; set; }
        public ITransformRuleValidator Validator { get; set; }

        public static Random rnd = new Random();
        public RandomStringTransformRule(int len, string path, string allowed = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789", string prefix = "")
        {
            Len = len;
            AllowedChars = allowed;
            Prefix = prefix;
            Path = path;
            Validator = new GenericValidator();
        }
        private string createRandomString()
        {
            char[] chars = new char[Len];
            for (int i = 0; i < Len; i++)
            {
                chars[i] = AllowedChars[rnd.Next(0, AllowedChars.Length)];
            }
            return $"{Prefix}{new string(chars)}";
        }

        public void transform(XmlNode node)
        {
            if (Validator.Validate(node))
                node.InnerText = createRandomString();
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteElementString("Type", RuleType.ToString());
            writer.WriteElementString("Length", Len.ToString());
            writer.WriteElementString("AllowedChars", AllowedChars);
            writer.WriteElementString("Prefix", Prefix);
        }

        public object Clone()
        {
            return new RandomStringTransformRule(Len, Path, AllowedChars, Prefix);
        }
    }
}
