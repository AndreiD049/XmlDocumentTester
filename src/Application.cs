using System;
using System.Collections.Generic;
using System.Text;
using XMLDocumentGenerator.Interfaces;

namespace XMLDocumentGenerator.src
{
    class Application : IApplication
    {
        public List<IXMLDocument> XmlDocuments { get; set; }

        public Application()
        {
            XmlDocuments = new List<IXMLDocument>();
        }

        public Application(IEnumerable<IXMLDocument> docs)
        {
            XmlDocuments = new List<IXMLDocument>(docs);
        }

        public void AddXmlDoc()
        {
            throw new NotImplementedException();
        }

        public void RemoveXmlDoc()
        {
            throw new NotImplementedException();
        }

        public void run()
        {
            Console.WriteLine("Application running");
        }
    }
}
