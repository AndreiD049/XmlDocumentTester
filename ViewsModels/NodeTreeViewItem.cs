using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.src;

namespace XmlTesterPresentation.ViewsModels
{
    public class NodeTreeViewItem: TreeViewItem
    {
        public XmlNode Node { get; set; }
        public string FullPath { get; set; } = null;
        public NodeTreeViewItem(XmlNode node = null)
        {
            Node = node;
            if (node.NodeType == XmlNodeType.Element || node.NodeType == XmlNodeType.Attribute)
                FullPath = Utils.getFullPath(node);
        }
    }
}
