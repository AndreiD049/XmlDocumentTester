using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src
{
    class TestCase : ITestCase, IXmlWriter
    {
        public TestCase(string Name, string SaveLocation, Dictionary<string, string> options = null)
        {
            this.Options = options != null ? options: new Dictionary<string, string>();
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            rules = new Dictionary<string, List<IXMLTransformRule>>();
            Document = null;
        }

        public TestCase(string Name, string SaveLocation, IDictionary<string, List<IXMLTransformRule>> rules, Dictionary<string, string> options = null)
        {
            this.Options = options != null ? options: new Dictionary<string, string>();
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            this.rules = new Dictionary<string, List<IXMLTransformRule>>(rules);
            Document = null;
        }

        public TestCase(string Name, IXMLDocument doc, string SaveLocation, Dictionary<string, string> options = null)
        {
            this.Options = options != null ? options: new Dictionary<string, string>();
            this.Name = Name;
            this.SaveLocation = SaveLocation;
            rules = new Dictionary<string, List<IXMLTransformRule>>();
            Document = doc;
        }

        public TestCase(string Name, IXMLDocument doc, string SaveLocation, IDictionary<string, List<IXMLTransformRule>> rules, Dictionary<string, string> options = null)
        {
            this.Options = options != null ? options: new Dictionary<string, string>();
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
        public Dictionary<string, string> Options { get; set; }
        public List<object> NodesToDelete { get; set; } = new List<object>();

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
            foreach(XmlNode del in NodesToDelete)
            {
                if (del.NodeType == XmlNodeType.Element)
                {
                    XmlNode parent = del.ParentNode;
                    if (parent != null)
                    {
                        parent.RemoveChild(del);
                    }
                }
                else if (del.NodeType == XmlNodeType.Attribute)
                {
                    XmlAttribute attr = del as XmlAttribute;
                    attr.OwnerElement.RemoveAttribute(attr.Name);
                }
            }
            NodesToDelete.Clear();
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
            // Write options
            writer.WriteStartElement("Options");
            foreach(string key in Options.Keys)
            {
                writer.WriteElementString(key, Options[key]);
            }
            writer.WriteEndElement();
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
        public void UpdateSequentialRules()
        {
            foreach(IXMLTransformRule rule in Utils.FlattenTransformRules(rules))
            {
                ISequentialRule seq_rule = rule as ISequentialRule;
                if (seq_rule != null)
                {
                    seq_rule.UpdateSequencialValues();
                }
            }
        }

        public void AddOption(string key, string value)
        {
            Options[key] = value;
        }

        public string GetOption(string key)
        {
            Options.TryGetValue(key, out string result);
            return result;
        }

        public object Clone()
        {
            Dictionary<string, string> opt_copy = new Dictionary<string, string>();
            // Copy the options
            foreach(string opt_key in Options.Keys)
            {
                opt_copy[opt_key] = Options[opt_key];
            }

            ITestCase clone = new TestCase(Name, Document, SaveLocation, new Dictionary<string, List<IXMLTransformRule>>(), opt_copy);
            // Copy the rules dictionary
            foreach(string rule_list_key in rules.Keys)
            {
                // for each rule of this key copy it
                foreach(IXMLTransformRule rule in rules[rule_list_key])
                {
                    clone.AddRule(rule_list_key, rule);
                }
            }

            return clone;
        }
    }
}
