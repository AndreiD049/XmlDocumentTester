using System;
using System.Collections.Generic;
using System.Xml;

namespace XmlTester.Interfaces
{
    public interface ITransformRuleValidator
    {
        public bool Validate(XmlNode node=null);
    }
}
