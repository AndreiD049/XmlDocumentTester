using System;
using System.Collections.Generic;
using System.Text;

namespace XmlTester.Interfaces
{
    public interface ISearchable
    {
        public void Search(string value);
        public void BeforeSearch();
    }
}
