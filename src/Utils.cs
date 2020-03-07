using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src
{
    static class Utils
    {
        private static readonly Regex numeric_regex = new Regex("[^0-9.-]+");
        public static string getFullPath(XmlNode node)
        {
            if (node == null)
                return "";
            return GetXPath_SequentialIteration(node);
        }

        public static string GetXPath_SequentialIteration(this XmlNode element)
        {
            string path = "";
            if (element.NodeType == XmlNodeType.Attribute)
            {
                path = "@" + element.Name;
                element = ((XmlAttribute)element).OwnerElement;
            }
            path = "/" + element.Name + path;


            XmlElement parentElement = element.ParentNode as XmlElement;
            if (parentElement != null)
            {
                // Gets the position within the parent element.
                // However, this position is irrelevant if the element is unique under its parent:
                XmlNodeList siblings = parentElement.SelectNodes(element.Name);
                if (siblings != null && siblings.Count > 1) // There's more than 1 element with the same name
                {
                    int position = 1;
                    foreach (XmlElement sibling in siblings)
                    {
                        if (sibling == element)
                            break;

                        position++;
                    }

                    path = path + "[" + position + "]";
                }

                // Climbing up to the parent elements:
                path = parentElement.GetXPath_SequentialIteration() + path;
            }
            return path;
        }

        /// <summary>
        /// Returns the number closest to n, that is divisible by m
        /// </summary>
        public static int closestNumber(int n, int m)
        {
            int q = n / m;
            int n1 = m * q;
            int n2 = (n * m) > 0 ? (m * (q + 1)) : (m * (q - 1));
            if (Math.Abs(n - n1) < Math.Abs(n - n2))
                return n1;
            return n2;
        }

        public static IEnumerable<XmlNode> getAllNodesEnumerable(XmlNode root)
        {
            List<XmlNode> result = new List<XmlNode>();
            Stack<XmlNode> stack = new Stack<XmlNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                XmlNode current = stack.Pop();
                result.Add(current);
                if (current.Attributes != null)
                {
                    foreach (XmlNode attr in current.Attributes)
                    {
                        result.Add(attr);
                    }
                }
                foreach (XmlNode n in current.ChildNodes)
                {
                    if (n.NodeType == XmlNodeType.Element)
                    {
                        stack.Push(n);
                    }
                }
            }
            return result;
        }

        public static List<IXMLTransformRule> FlattenTransformRules(Dictionary<string, List<IXMLTransformRule>> dict)
        {
            List<IXMLTransformRule> result = new List<IXMLTransformRule>();
            foreach (List<IXMLTransformRule> rules in dict.Values)
                result.AddRange(rules);
            return result;
        }
        
        public static bool IsNumeric(string value)
        {
            return !numeric_regex.IsMatch(value);
        }

        public static string GetNewDocName()
        {
            return $@"{Guid.NewGuid()}.xml";
        }

        public static void CopyXmlDocWithNewName(IXMLDocument doc)
        {
            string newPath = Path.Combine(doc.App.DataFolder, doc.App.DocumentsFolder, GetNewDocName());
            doc.FullPath = newPath;
            doc.Save(newPath);
        }

        public static void checkCreateFolder(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
