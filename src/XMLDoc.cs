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
        public XmlDocument doc { get; }
        public XMLDoc(string path)
        {
            doc = new XmlDocument();
            doc.Load(path);
            fullPath = path;
            root = doc.DocumentElement;
            testCases = new List<ITestCase>();
        }

        public XMLDoc(string path, IEnumerable<ITestCase> tests)
        {
            doc = new XmlDocument();
            doc.Load(path);
            fullPath = path;
            root = doc.DocumentElement;
            testCases = new List<ITestCase>(tests);
            // set the testcase document for all tests
            foreach (ITestCase t in testCases)
            {
                t.setDocument(this);
            }
        }

        public object Clone()
        {
            XMLDoc clone = new XMLDoc(fullPath, testCases);
            return clone;
        }

        public void Save(string path)
        {
            doc.Save(path);
        }
    }
}
