using System;
using System.Collections.Generic;
using System.Text;

namespace XMLDocumentGenerator.Interfaces
{
    interface ITestCase
    {
        public string Name { get; set; }
        public string SaveLocation { get; set; }
        /// <summary>
        /// Reference to the IXMLDocument to work with
        /// </summary>
        public IXMLDocument Document { get; set; }
        /// <summary>
        /// Generates the XMLDocument by following the rules defined.
        /// If no rules defined. It should just copy the document
        /// and saves it on the save location
        /// </summary>
        /// <returns>new IXMLDocument</returns>
        public void generate();
        /// <summary>
        /// Hash table of Paths - Rules
        /// </summary>
        public Dictionary<string, IXMLTransformRule> rules { get; set; }

        public void AddRule(string key, IXMLTransformRule factory);

        public void RemoveRule(string key);

        public void setDocument(IXMLDocument doc);
    }
}
