using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src
{
    class TestCase : ITestCase, IXmlWriter
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
        public IXMLDocument TransformedDocument { get; set; }

        public bool AddRule(string key, IXMLTransformRule rule)
        {
            bool result = rules.TryAdd(key, rule);
            return result;
        }

        public void RemoveRule(string key)
        {
            rules.Remove(key);
        }

        public void generate()
        {
            IXMLDocument newDoc = (IXMLDocument)Document.Clone();
            foreach (XmlNode x in Utils.getAllNodesEnumerable(Document.Root))
            {
                string fullPath = Utils.getFullPath(x);
                if (rules.ContainsKey(fullPath))
                {
                    rules[fullPath].transform(x);
                }
            }
            TransformedDocument = newDoc;
        }

        public void setDocument(IXMLDocument doc)
        {
            Document = doc;
        }

        public void writeXml(XmlWriter writer)
        {
            writer.WriteStartElement("TestCase");
            // Write the testCase Name and rules
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("SaveLocation", SaveLocation);
            // Rules
            writer.WriteStartElement("Rules");
            foreach (string rule in rules.Keys)
            {
                writer.WriteStartElement("Rule");
                writer.WriteElementString("Path", rule);
                ((IXmlWriter)rules[rule]).writeXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public void SaveOnLocation()
        {
            TransformedDocument.Save(Path.Combine(SaveLocation, $"{Name} - {DateTime.Now.Ticks}.xml"));
        }
    }
}
