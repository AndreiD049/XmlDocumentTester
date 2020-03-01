using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

public enum TransformRuleTypes { Fixed, Random, RepeatNode }

namespace XmlTesterPresentation.Interfaces
{
    /// <summary>
    /// a rule to transform an XML node
    /// </summary>
    public interface IXMLTransformRule: ICloneable
    {
        public ITestCase Parent { get; set; }
        public ITransformRuleValidator Validator { get; set; }
        public TransformRuleTypes RuleType { get; set; }
        public string Path { get; set; }
        public void transform(XmlNode node);
    }
}
