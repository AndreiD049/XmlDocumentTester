using System;
using System.Collections.Generic;
using System.Text;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.Interfaces
{
    public interface IApplication
    {
        public string TestSuiteFolder { get; set; }
        public string DocumentsFolder { get; set; }
        public List<IXMLDocument> XmlDocuments { get; set; }
        public string DataFolder { get; set; }
        public void run();
        public void AddXmlDoc(IXMLDocument doc);
        public void AddXmlDoc(string path);
        public void RemoveXmlDoc(IXMLDocument doc);
    }
}
