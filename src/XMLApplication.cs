using System;
using System.Collections.Generic;
using XmlTester.Interfaces;
using System.IO;

namespace XmlTester.src
{
    class XMLApplication : IApplication
    {
        public string TestSuiteFolder { get; set; } = "TestSuites";
        public string DocumentsFolder { get; set; } = "Documents";
        public List<IXMLDocument> XmlDocuments { get; set; }
        public string DataFolder { get; set; }

        public XMLApplication(string DataFolder = "./data")
        {
            XmlDocuments = new List<IXMLDocument>();
            this.DataFolder = Path.GetFullPath(DataFolder);
            Utils.checkCreateFolder(DataFolder);
            Utils.checkCreateFolder(Path.Combine(DataFolder, TestSuiteFolder));
            Utils.checkCreateFolder(Path.Combine(DataFolder, DocumentsFolder));
        }

        public XMLApplication(IEnumerable<IXMLDocument> docs, string DataFolder = "./data")
        {
            XmlDocuments = new List<IXMLDocument>(docs);
            this.DataFolder = DataFolder;
        }

        public void AddXmlDoc(IXMLDocument doc)
        {
            string newFile = Path.Combine(DataFolder, DocumentsFolder, Path.GetFileName(doc.FullPath));
            if (!File.Exists(newFile))
                File.Copy(doc.FullPath, newFile);
            doc.FullPath = newFile;
            XmlDocuments.Add(doc);
        }

        public void AddXmlDoc(string path)
        {
            string newFile = Path.Combine(DataFolder, DocumentsFolder, Path.GetFileName(path));
            if (!File.Exists(newFile))
                File.Copy(path, newFile);
            XmlDocuments.Add(new XMLDoc(newFile, this));
        }

        public void RemoveXmlDoc(IXMLDocument doc)
        {
            // remove the test suite file
            File.Delete(Path.Combine(this.DataFolder, TestSuiteFolder, Path.GetFileName(doc.FullPath)));
            File.Delete(Path.Combine(this.DataFolder, DocumentsFolder, Path.GetFileName(doc.FullPath)));
            XmlDocuments.Remove(doc);
        }
    }
}
