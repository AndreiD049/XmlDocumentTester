using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XMLDocumentGenerator.Interfaces
{
    /// <summary>
    /// a rule to transform an XML node
    /// </summary>
    interface IXMLTransformRule
    {
        public void transform(XmlNode node);
    }
}
