using System;
using XMLDocumentGenerator.src;
using XMLDocumentGenerator.src.TransformRules;
using XMLDocumentGenerator.Interfaces;
using System.Collections.Generic;

namespace XMLDocumentGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCase test1 = new TestCase("MyTest", @"C:\Users\Андрей\source\repos\XmlDocumentTester\data\sample3.xml");
            test1.AddRule("Order.Customer", new FixedStringTransformRule("PESABIC"));
            test1.AddRule("Order.CustomerReference1", new RandomStringTransformRule(7, "1234567890", "309"));
            test1.AddRule("Order.CustomerReference2", new RandomStringTransformRule(10));
            test1.AddRule("Order.CustomerReference3", new RandomStringTransformRule(10));
            TestCase[] cases = new TestCase[] { test1,
                                                new TestCase("Two Batches", @"C:\Users\Андрей\source\repos\XmlDocumentTester\data\sample4.xml")};
            XMLDoc doc1 = new XMLDoc(@"C:\Users\Андрей\source\repos\XmlDocumentTester\data\sample.xml", cases);
            XMLDoc[] items = new XMLDoc[] { doc1 };
            IApplication app = new Application(items);
            app.run();
            app.XmlDocuments[0].testCases[0].generate();
        }
    }
}
