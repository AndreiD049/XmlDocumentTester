using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;

namespace XmlTester.Interfaces
{
    public interface ITreeElement
    {
        public bool DefExpanded { get; set; }
        public bool Expanded { get; set; }
        public bool Enabled { get; set; }
        XmlNode Node { get; set; }
        ObservableCollection<ITreeElement> ChildNodes { get; set; }
        public string Name { get; set; }
        public void ParseChildren(XmlNodeList children, int level);
        public void ExpandNode(int level = 0);
        public void ChainExpand();
    }
}
