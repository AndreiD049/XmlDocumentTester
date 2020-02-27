using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlTesterPresentation.Interfaces
{
    public interface IXMLDocument: ICloneable
    {
        public IApplication App { get; }
        public string FullPath { get; set; }
        public string Name { get; set; }
        public XmlNode Root { get; set; }
        public List<ITestCase> TestCases { get; set; }
        public void AddTestCase(ITestCase testCase);
        public void RemoveTestCase(ITestCase testCase);
        public void Save(string path);
        public ITestSuiteSaver TestSuiteSaver { get; set; }

    }
}
