﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using XmlTester.Interfaces;

namespace XmlTester.src
{
    class TestCaseParser
    {
        public static void Parse(XmlNode TestSuiteDoc, IXMLDocument resultDoc)
        {
            XmlNodeList testCases = TestSuiteDoc.SelectNodes("//TestCase");
            foreach (XmlNode node in testCases)
            {
                XmlNode name = node.SelectSingleNode("Name");
                XmlNode saveLocation = node.SelectSingleNode("SaveLocation");
                XmlNode optionsNode = node.SelectSingleNode("Options");
                if (name == null)
                {
                    Console.Error.WriteLine($"Name is missing for a TestCase of {resultDoc.FullPath}");
                    continue;
                }
                if (saveLocation == null)
                {
                    Console.Error.WriteLine($"SaveLocation is missing for a TestCase of {resultDoc.FullPath}");
                    continue;
                }
                if (!Directory.Exists(saveLocation.InnerText))
                {
                    Console.Error.WriteLine($"Dirrectory {saveLocation.InnerText} doesn't exist. Skipping");
                    continue;
                }

                // parse the options
                Dictionary<string, string> options = new Dictionary<string, string>();
                if (optionsNode != null)
                {
                    foreach(XmlNode opt in optionsNode.ChildNodes)
                    {
                        if (opt.NodeType == XmlNodeType.Element)
                            options[opt.LocalName] = opt.InnerText;
                    }
                }
                
                // Esle create the testCase
                ITestCase testCase = new TestCase(name.InnerText, resultDoc, saveLocation.InnerText, options);
                // Then, parse all the rules in this testcase
                foreach(XmlNode rule in node.SelectNodes("Rules/Rule"))
                {
                    TransformRuleParser.Parse(rule, testCase);
                }
                resultDoc.TestCases.Add(testCase);
            }
        }
    }
}
