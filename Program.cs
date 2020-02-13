using System;
using System.Xml;
using XMLDocumentGenerator.src;

namespace XMLDocumentGenerator
{
    class Program
    {
        public static string getFullPath(XmlNode node, string delimiter = "/")
        {
            if (node.ParentNode is null || node.ParentNode.NodeType == XmlNodeType.Document)
            {
                return node.Name;
            }
            else if (node.NodeType != XmlNodeType.Element)
            {
                return getFullPath(node.ParentNode, delimiter);
            }
            else
            {
                return getFullPath(node.ParentNode, delimiter) + $"{delimiter}{node.Name}";
            }
        }
        static void Main(string[] args)
        {
            XMLDoc[] items = new XMLDoc[] { new XMLDoc(@"C:\Users\я\source\repos\XMLDocumentGenerator\XMLDocumentGenerator\data\sample.xml"), 
                                            new XMLDoc(@"C:\Users\я\source\repos\XMLDocumentGenerator\XMLDocumentGenerator\data\sample.xml")};
            IApplication app = new Application(items);
            app.run();
            Console.WriteLine(((XMLDoc)app.XmlDocuments[0]).root.ChildNodes[0].ChildNodes[0].Value);
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\я\source\repos\XMLDocumentGenerator\XMLDocumentGenerator\data\sample.xml");
            Console.WriteLine(getFullPath(doc.DocumentElement.ChildNodes[2].ChildNodes[0], "."));
        }
    }
}
