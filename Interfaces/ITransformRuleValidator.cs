using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlTesterPresentation.Interfaces
{
    public interface ITransformRuleValidator
    {
        public bool Validate(XmlNode rule);
    }
}
