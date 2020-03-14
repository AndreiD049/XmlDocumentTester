using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XmlTester.UIControls.VisualTreeElements
{
    abstract class GenericVisualTreeElement: ITreeElement, INotifyPropertyChanged
    {
        private bool initialExpanded;
        public bool DefExpanded { get; set; }
        public bool Expanded
        {
            get
            {
                return this.initialExpanded;
            }
            set
            {
                this.initialExpanded = value;
                OnPropertyChanged("Expanded");
            }
        }
        public bool Enabled { get; set; } = true;
        public XmlNode Node { get; set; }
        public ObservableCollection<ITreeElement> ChildNodes { get; set; }
        public string Name { get; set; }

        public GenericVisualTreeElement()
        {
        }

        public void ParseChildren(XmlNodeList children, int level)
        {
            if (level < 1)
            {
                foreach (XmlNode attr in Node.Attributes)
                {
                    ChildNodes.Add(new AttributeVisualTreeElement(attr, level + 1));
                }
                foreach (XmlNode child in children)
                {
                    switch(child.NodeType)
                    {
                        case XmlNodeType.Element:
                            ChildNodes.Add(new NodeVisualTreeElement(child, level+1, DefExpanded));
                            break;
                        case XmlNodeType.Text:
                            ChildNodes.Add(new TextVisualTreeElement(child, level+1));
                            break;
                    }
                }
            }
            else
            {
                if (Node.ChildNodes.Count != 0 || Node.Attributes != null)
                    // Add a null node to display the expander
                    ChildNodes.Add(null);
            }
        }

        public void ExpandNode(int level = 0)
        {
            this.Expanded = true;
            if (ChildNodes.Count > 0 && ChildNodes[0] == null)
            {
                ChildNodes.Clear();
                ParseChildren(Node.ChildNodes, level);
            }
        }

        public void ChainExpand()
        {
            Expanded = true;
            ExpandNode();
            foreach(ITreeElement child in ChildNodes)
            {
                child.ChainExpand();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
