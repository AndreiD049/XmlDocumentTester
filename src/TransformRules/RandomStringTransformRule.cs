using System;
using System.Collections.Generic;
using System.Xml;
using XMLDocumentGenerator.Interfaces;

namespace XMLDocumentGenerator.src
{
    class RandomStringTransformRule : IXMLTransformRule
    {
        public int Len { get; }
        public XmlNode Node { get; }
        public string AllowerChars { get; set; }
        public string Prefix { get; set; }
        public static Random rnd = new Random();
        public RandomStringTransformRule(int len, string allowed = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789", string prefix = "")
        {
            Len = len;
            AllowerChars = allowed;
            Prefix = prefix;
        }
        private string createRandomString()
        {
            char[] chars = new char[Len];
            for (int i = 0; i < Len; i++)
            {
                chars[i] = AllowerChars[rnd.Next(0, AllowerChars.Length)];
            }
            return $"{Prefix}{new string(chars)}";
        }

        public void transform(XmlNode node)
        {
            node.InnerText = createRandomString();
        }
    }
}
