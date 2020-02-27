using System;
using System.IO;
using XmlTesterPresentation.Interfaces;
using System.Xml;

namespace XmlTesterPresentation.src
{
    static class XmlDocLoader
    {
        public static void LoadXmlDocuments(IApplication app)
        {
            string[] files = new string[0];
            string TestSuitePath = Path.Combine(app.DataFolder, app.TestSuiteFolder);
            try
            {
                files = Directory.GetFiles(TestSuitePath, "*.xml");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.Error.WriteLine($"Dirrectory not found - {TestSuitePath}");
                throw e;
            }
            foreach (string file in files)
            {
                XmlDocument TestSuiteDoc = new XmlDocument();
                TestSuiteDoc.Load(file);
                XmlNode docName = TestSuiteDoc.DocumentElement.SelectSingleNode("//Document/Name");
                XmlNode el = TestSuiteDoc.DocumentElement.SelectSingleNode("//Document/FullPath");
                Console.WriteLine($"raw file is at {el.InnerText}");
                // check if the path is valid
                if (File.Exists(el.InnerText))
                {
                    IXMLDocument doc = new XMLDoc(el.InnerText, app, docName?.InnerText);
                    TestCaseParser.Parse(TestSuiteDoc, doc);
                    app.AddXmlDoc(doc);
                }
                else
                {
                    Console.Error.WriteLine($"Path doesn't exists or file was deleted: {el.InnerText}");
                    continue;
                }
            }
        }
    }
}
