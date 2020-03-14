using System;
using System.Collections.Generic;
using System.Text;
using XmlTester.Interfaces;

namespace XmlTester.Interfaces
{
    public interface IApplication
    {
        public string TestSuiteFolder { get; set; }
        public string DocumentsFolder { get; set; }
        public List<IXMLDocument> XmlDocuments { get; set; }
        public string DataFolder { get; set; }
        public void AddXmlDoc(IXMLDocument doc);
        public void AddXmlDoc(string path);
        public void RemoveXmlDoc(IXMLDocument doc);
    }
}
