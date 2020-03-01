using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace XmlTesterPresentation.Interfaces
{
    public interface ITransformRuleProps
    {
        public TextBox xPath { get; }
        public void Save();
    }
}
