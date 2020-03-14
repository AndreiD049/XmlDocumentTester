using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using XmlTester.UIControls;

namespace XmlTester.Interfaces
{
    public interface ITreeViewQueuedAction
    {
        public bool Act();
        public RuleTreeControl RuleTreeControl { get; set; }
    }
}
