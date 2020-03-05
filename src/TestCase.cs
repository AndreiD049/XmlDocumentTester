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
            rules = new Dictionary<string, List<IXMLTransformRule>>();
            Document = null;
        }

        public TestCase(string Name, string SaveLocation, IDictionary<string, List<IXMLTransformRule>> rules)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            this.rules = new Dictionary<string, List<IXMLTransformRule>>(rules);
            Document = null;
        }

        public TestCase(string Name, IXMLDocument doc, string SaveLocation)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            rules = new Dictionary<string, List<IXMLTransformRule>>();
            Document = doc;
        }

        public TestCase(string Name, IXMLDocument doc, string SaveLocation, IDictionary<string, List<IXMLTransformRule>> rules)
        {
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            this.rules = new Dictionary<string, List<IXMLTransformRule>>(rules);
            Document = doc;
        }

        public string Name { get; set; }
        public string SaveLocation { get; set; }
        public Dictionary<string, List<IXMLTransformRule>> rules { get; set; }
        public IXMLDocument Document { get; set; }
        public IXMLDocument TransformedDocument { get; set; }

        public void AddRule(string key, IXMLTransformRule rule)
        {
            if (rules.ContainsKey(key))
                rules[key].Add(rule);
            else
                rules[key] = new List<IXMLTransformRule>() { rule };
            rule.Parent = this;
        }

        public void RemoveRule(string key, IXMLTransformRule rule)
        {
            if (rules.ContainsKey(key))
                rules[key].Remove(rule);
        }

        public void generate()
        {
            IXMLDocument newDoc = (IXMLDocument)Document.Clone();
            TransformedDocument = newDoc;
            foreach (XmlNode x in Utils.getAllNodesEnumerable(TransformedDocument.Root))
            {
                string fullPath = Utils.getFullPath(x);
                if (rules.ContainsKey(fullPath))
                {
                    // rules[fullPath] is the list of rules
                    foreach (IXMLTransformRule rule in rules[fullPath])
                        rule.transform(x);
                }
            }
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
            List<IXMLTransformRule> flattened_rules = Utils.FlattenTransformRules(rules);
            foreach (IXMLTransformRule rule in flattened_rules)
            {
                writer.WriteStartElement("Rule");
                writer.WriteElementString("Path", rule.Path);
                ((IXmlWriter)rule).writeXml(writer);
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
