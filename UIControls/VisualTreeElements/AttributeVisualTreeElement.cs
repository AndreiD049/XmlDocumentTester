using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;
using XmlTester.Interfaces;

namespace XmlTester.UIControls.VisualTreeElements
{
    class AttributeVisualTreeElement: GenericVisualTreeElement
    {
        public AttributeVisualTreeElement(XmlNode node, int level)
        {
            this.Node = node;
            this.Name = $"@{node.Name}: {node.InnerText}";
            this.ChildNodes = new ObservableCollection<ITreeElement>();
        }
    }
}
