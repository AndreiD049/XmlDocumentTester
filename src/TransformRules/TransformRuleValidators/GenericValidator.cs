﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XmlTester.Interfaces;

namespace XmlTester.src.TransformRules.TransformRuleValidators
{
    class GenericValidator : ITransformRuleValidator
    {
        /// <summary>
        /// Validate Everything!
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public bool Validate(XmlNode node)
        {
            return true;
        }
    }
}
