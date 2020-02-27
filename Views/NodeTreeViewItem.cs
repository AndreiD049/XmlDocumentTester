using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows;
using System.Windows.Controls;

namespace XmlTesterPresentation.Views
{
    class NodeTreeViewItem: TreeViewItem
    {
        public XmlNode Node { get; set; }
        public NodeTreeViewItem(XmlNode node = null): base()
        {
            Node = node;
        }
    }
}
