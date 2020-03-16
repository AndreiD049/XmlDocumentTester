using System;
using System.Collections.Generic;
using System.IO;
using XmlTester.Interfaces;
using System.Xml;

namespace XmlTester.src
{
    class XMLDoc:IXMLDocument, IXmlWriter
    {
        private XmlNode root;
        public XmlDocument Document { get; set; }
        public IApplication App { get; }
        public string FullPath { get; set; }
        public string Name { get; set; }
        public XmlNode Root
        {
            get
            {
                if (root == null)
                {
                    Document.Load(FullPath);
                    root = Document.DocumentElement;
                }
                return root;
            }
            set
            {
                root = value;
            }
        }
        public List<ITestCase> TestCases { get; set; }
        public ITestSuiteSaver TestSuiteSaver { get; set; }
        public XMLDoc(string path, IApplication app, string name = "")
        {
            this.App = app;
            Document = new XmlDocument();
            //Document.Load(path);
            FullPath = path;
            if (name == string.Empty)
                Name = Path.GetFileName(path);
            else
                Name = name;
            //Root = doc.DocumentElement;
            TestCases = new List<ITestCase>();
            TestSuiteSaver = new TestSuiteSaver(Path.Combine(app.DataFolder, app.TestSuiteFolder), this);
        }

        public XMLDoc(string path, IEnumerable<ITestCase> tests, IApplication app, string name = "")
        {
            this.App = app;
            Document = new XmlDocument();
            FullPath = path;
            if (name == string.Empty)
                Name = Path.GetFileName(path);
            else
                Name = name;
            //Root = doc.DocumentElement;
            TestCases = new List<ITestCase>(tests);
            TestSuiteSaver = new TestSuiteSaver(Path.Combine(app.DataFolder, app.TestSuiteFolder), this);
            // set the testcase document for all tests
            foreach (ITestCase t in TestCases)
            {
                t.setDocument(this);
            }
        }

        public object Clone()
        {
            XMLDoc clone = new XMLDoc(FullPath, TestCases, App, Name);
            return clone;
        }

        public void Save(string path)
        {
            Root.OwnerDocument.Save(path);
        }

        public void AddTestCase(ITestCase testCase)
        {
            TestCases.Add(testCase);
            TestSuiteSaver.SaveSuite();
        }

        public void RemoveTestCase(ITestCase testCase)
        {
            TestCases.Remove(testCase);
            TestSuiteSaver.SaveSuite();
        }

        public void writeXml(XmlWriter writer)
        {
            // Copy the document to a safe location
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("FullPath", FullPath);
        }
    }
}
