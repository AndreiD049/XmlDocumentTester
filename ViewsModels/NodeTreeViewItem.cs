using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows;
using System.Windows.Controls;

namespace XmlTesterPresentation.ViewsModels
{
    class NodeTreeViewItem: TreeViewItem
    {
        public XmlNode Node { get; set; }
        public NodeTreeViewItem(XmlNode node = null)
        {
            Node = node;
        }
    }
}
