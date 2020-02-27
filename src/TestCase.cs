using System;
using System.Collections.Generic;
using System.Xml;
using XMLDocumentGenerator.Interfaces;

namespace XMLDocumentGenerator.src
{
    class TestCase : ITestCase
    {
        public TestCase(string Name, string SaveLocation)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            rules = new Dictionary<string, IXMLTransformRule>();
            Document = null;
        }

        public TestCase(string Name, string SaveLocation, IDictionary<string, IXMLTransformRule> rules)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            this.rules = new Dictionary<string, IXMLTransformRule>(rules);
            Document = null;
        }

        public TestCase(string Name, IXMLDocument doc, string SaveLocation)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            rules = new Dictionary<string, IXMLTransformRule>();
            Document = doc;
        }

        public TestCase(string Name, IXMLDocument doc, string SaveLocation, IDictionary<string, IXMLTransformRule> rules)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            this.rules = new Dictionary<string, IXMLTransformRule>(rules);
            Document = doc;
        }

        public string Name { get; set; }
        public string SaveLocation { get; set; }
        public Dictionary<string, IXMLTransformRule> rules { get; set; }
        public IXMLDocument Document { get; set; }

        public void AddRule(string key, IXMLTransformRule rule)
        {
            rules.Add(key, rule);
        }

        public void RemoveRule(string key)
        {
            rules.Remove(key);
        }

        public void generate()
        {
            IXMLDocument newDoc = (IXMLDocument)Document.Clone();
            foreach (XmlNode x in Utils.getAllNodesEnumerable(Document.root))
            {
                string fullPath = Utils.getFullPath(x);
                if (rules.ContainsKey(fullPath))
                {
                    rules[fullPath].transform(x);
                }
            }
            newDoc.Save(SaveLocation);
        }

        public void setDocument(IXMLDocument doc)
        {
            Document = doc;
        }
    }
}
