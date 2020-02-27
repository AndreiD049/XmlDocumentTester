using System;
using System.Collections.Generic;
using System.Xml;

namespace XMLDocumentGenerator.src
{
    static class Utils
    {
        public static string getFullPath(XmlNode node, string delimiter = ".")
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

        public static IEnumerable<XmlNode> getAllNodesEnumerable(XmlNode root)
        {
            List<XmlNode> result = new List<XmlNode>();
            Stack<XmlNode> stack = new Stack<XmlNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                XmlNode current = stack.Pop();
                result.Add(current);
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
    }
}
