using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.UIControls.VisualTreeElements
{
    class NodeVisualTreeElement : GenericVisualTreeElement
    {
        public NodeVisualTreeElement(XmlNode node, int level, bool expanded = false)
        {
            this.Node = node;
            this.Name = node.Name;
            this.ChildNodes = new ObservableCollection<ITreeElement>();
            DefExpanded = expanded;
            Expanded = level == 0 ? true : expanded;
            ParseChildren(node.ChildNodes, level);
        }
    }
}
