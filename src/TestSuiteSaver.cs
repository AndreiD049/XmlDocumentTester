using XmlTesterPresentation.Interfaces;
using System.Xml;
using System.IO;
using System;

namespace XmlTesterPresentation.src
{
    class TestSuiteSaver: ITestSuiteSaver
    {
        public IXMLDocument Document { get; set; }
        public string savePath { get; set; }
        public TestSuiteSaver(string savePath, IXMLDocument doc)
        {
            this.savePath = savePath;
            Document = doc;
        }
        /// <summary>
        /// For the xml docuemnt with the original document in it, all the TestCases and rules.
        /// </summary>
        public void SaveSuite()
        {
            string SuiteName = Path.GetFileName(Document.FullPath);
            // set Xml settings for writing
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create(Path.Combine(savePath, SuiteName), settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("TestSuite");
            writer.WriteElementString("Name", SuiteName);
            writer.WriteStartElement("Document");
            // read the document in order to insert in the testSuite
            ((IXmlWriter)Document).writeXml(writer);
            writer.WriteEndElement();
            // Test Cases
            writer.WriteStartElement("TestCases");
            foreach(ITestCase TestCase in Document.TestCases)
            {
                ((IXmlWriter)TestCase).writeXml(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
    }
}
