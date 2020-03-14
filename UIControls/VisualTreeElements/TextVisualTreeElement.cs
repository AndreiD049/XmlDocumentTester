using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;
using XmlTester.Interfaces;

namespace XmlTester.UIControls.VisualTreeElements
{
    class TextVisualTreeElement: GenericVisualTreeElement
    {
        public TextVisualTreeElement(XmlNode node, int level)
        {
            this.Enabled = false;
            this.Node = node;
            this.Name = Node.InnerText;
            this.ChildNodes = new ObservableCollection<ITreeElement>();
        }
    }
}
