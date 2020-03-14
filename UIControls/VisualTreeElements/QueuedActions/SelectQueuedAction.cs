using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Controls;
using XmlTester.Interfaces;

namespace XmlTester.UIControls.VisualTreeElements.QueuedActions
{
    class SelectQueuedAction : ITreeViewQueuedAction
    {
        public string Path { get; set; }
        public RuleTreeControl RuleTreeControl { get; set; }
        public SelectQueuedAction(string path, RuleTreeControl rtc)
        {
            Path = path;
            RuleTreeControl = rtc;
        }

        public bool Act()
        {
            RuleTreeControl.NodeMap.TryGetValue(Path, out TreeViewItem tw);
            XmlNode node = (tw?.DataContext as ITreeElement)?.Node;
            if (node != null)
            {
                tw.IsSelected = true;
                tw.BringIntoView();
                return true;
            }
            return false;
        }
    }
}
