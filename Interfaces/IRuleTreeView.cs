using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Controls;

namespace XmlTester.Interfaces
{
    interface IRuleTreeView
    {
        public Dictionary<string, TreeViewItem> NodeMap { get; set; }
        public Dictionary<string, ITreeElement> PreviousState { get; set; }
        public ITreeElement Root { get; set; }
        public void SelectNode(string path);
        public void ExpandNodeOnPath(string path);
        public void Update();
    }
}
