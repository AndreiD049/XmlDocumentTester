using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using XmlTester.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace XmlTester.src
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
                path = $"/@{element.Name}";
                element = ((XmlAttribute)element).OwnerElement;
                return element.GetXPath_SequentialIteration() + path;
            }
            path = "/" + element.Name + path;

            XmlElement parentElement = element.ParentNode as XmlElement;
            if (parentElement != null && (element.NodeType == XmlNodeType.Element || element.NodeType == XmlNodeType.Attribute))
            {
                // Gets the position within the parent element.
                // However, this position is irrelevant if the element is unique under its parent:
                // Use GetElementsByTagName instead of SelectNodes (used for XmlDocuments with namespaces)
                XmlNodeList siblings = parentElement.GetElementsByTagName(element.Name);
                int position = 1;
                foreach (XmlElement sibling in siblings)
                {
                    if (sibling == element)
                        break;

                    position++;
                }

                path = path + "[" + position + "]";

                // Climbing up to the parent elements:
                path = parentElement.GetXPath_SequentialIteration() + path;
            }
            return path;
        }

        public static XmlNode SelectSingleNode(XmlNode node, string xpath)
        {
            if (node.NamespaceURI != string.Empty)
            {
                XmlNamespaceManager mgr = new XmlNamespaceManager(node.OwnerDocument.NameTable);
                mgr.AddNamespace(node.GetPrefixOfNamespace(node.NamespaceURI), node.NamespaceURI);
                return node.SelectSingleNode(xpath, mgr);
            }
            else
            {
                // if cannot return find this path, just skip it
                try
                {
                    return node.SelectSingleNode(xpath);
                }
                catch
                {
                    return null;
                }
            }
        }

        public static XmlNode GetParentNode(XmlNode node)
        {
            switch (node?.NodeType)
            {
                case XmlNodeType.Element:
                case XmlNodeType.Text:
                    return node.ParentNode;
                case XmlNodeType.Attribute:
                    return ((XmlAttribute)node).OwnerElement;
            }
            return null;
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


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }
                    foreach(T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
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
