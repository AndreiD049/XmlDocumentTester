using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Controls;
using XmlTester.Interfaces;
using XmlTester.src;

namespace XmlTester.UIControls.VisualTreeElements.QueuedActions
{
    class ExpandQueuedAction : ITreeViewQueuedAction
    {
        public string Path { get; set; }
        public RuleTreeControl RuleTreeControl { get; set; }

        public ExpandQueuedAction(string path, RuleTreeControl rtc)
        {
            Path = path;
            RuleTreeControl = rtc;
        }
        /// <summary>
        ///     Expands the item on path, only if it's present in current item
        /// </summary>
        /// <param name="item"></param>
        public bool Act()
        {
            RuleTreeControl.NodeMap.TryGetValue(Path, out TreeViewItem tw);
            XmlNode node = (tw?.DataContext as ITreeElement)?.Node;
            if (node != null)
            {
                tw.IsExpanded = true;
                return true;
            }
            return false;
        }
    }
}
