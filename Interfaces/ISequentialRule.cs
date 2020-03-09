using System;
using System.Collections.Generic;
using System.Text;

namespace XmlTester.Interfaces
{
    /// <summary>
    /// A rule whose values need to be updated only when a file is really generated.
    /// </summary>
    interface ISequentialRule
    {
        public void UpdateSequencialValues();
    }
}
