using System;
using System.Collections.Generic;
using System.Text;
using XMLDocumentGenerator.Interfaces;

namespace XMLDocumentGenerator
{
    interface IApplication
    {
        public List<IXMLDocument> XmlDocuments { get; set; }
        public void run();
        public void AddXmlDoc();
        public void RemoveXmlDoc();
    }
}
