using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlTesterPresentation.Interfaces
{
    public interface ITestCase
    {
        public string Name { get; set; }
        public string SaveLocation { get; set; }
        /// <summary>
        /// Reference to the IXMLDocument to work with
        /// </summary>
        public IXMLDocument Document { get; set; }
        public IXMLDocument TransformedDocument { get; set; }
        /// <summary>
        /// Generates the XMLDocument by following the rules defined.
        /// If no rules defined. It should just copy the document
        /// and saves it on the save location
        /// </summary>
        /// <returns>new IXMLDocument</returns>
        public void generate();
        /// <summary>
        /// Saves the TransformedDocument on SaveLocation
        /// </summary>
        public void SaveOnLocation();
        /// <summary>
        /// Hash table of Paths - Rules
        /// </summary>
        public Dictionary<string, List<IXMLTransformRule>> rules { get; set; }

        public void AddRule(string key, IXMLTransformRule factory);

        public void RemoveRule(string key, IXMLTransformRule rule);

        public void setDocument(IXMLDocument doc);
    }
}
