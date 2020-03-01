using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using XmlTesterPresentation.Interfaces;

namespace XmlTesterPresentation.src
{
    static class Utils
    {
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
