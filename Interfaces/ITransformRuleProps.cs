using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace XmlTesterPresentation.Interfaces
{
    public interface ITransformRuleProps
    {
        public IXMLTransformRule Rule { get; set; }
        public IXMLTransformRule Copy { get; set; }
        public void Update();
        public void Save();
    }
}
