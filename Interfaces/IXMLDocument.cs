using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLDocumentGenerator.Interfaces
{
    interface IXMLDocument: ICloneable
    {
        public string fullPath { get; set; }
        public XmlNode root { get; set; }
        public List<ITestCase> testCases { get; set; }
        public void Save(string path);

    }
}
