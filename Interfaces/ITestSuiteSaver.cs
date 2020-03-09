
namespace XmlTester.Interfaces
{
    public interface ITestSuiteSaver
    {
        public IXMLDocument Document { get; set; }
        public string savePath { get; set; }
        public void SaveSuite();
    }
}
