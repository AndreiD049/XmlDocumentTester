using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public enum TransformRuleTypes { Fixed, Random }

namespace XmlTesterPresentation.Interfaces
{
    /// <summary>
    /// a rule to transform an XML node
    /// </summary>
    public interface IXMLTransformRule
    {
        public TransformRuleTypes RuleType { get; set; }
        public string Path { get; set; }
        public void transform(XmlNode node);
    }
}
