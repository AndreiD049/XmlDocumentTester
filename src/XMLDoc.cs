using System;
using System.Collections.Generic;
using System.Text;
using XMLDocumentGenerator.Interfaces;
using System.Xml;

namespace XMLDocumentGenerator.src
{
    class XMLDoc:IXMLDocument
    {
        public string fullPath { get; set; }
        public XmlNode root { get; set; }
        public List<ITestCase> testCases { get; set; }
        public XMLDoc(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            fullPath = path;
            root = doc.DocumentElement;
            testCases = new List<ITestCase>();
        }

        public XMLDoc(string path, IEnumerable<ITestCase> tests)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            fullPath = path;
            root = doc.DocumentElement;
            testCases = new List<ITestCase>(tests);
        }
    }
}
