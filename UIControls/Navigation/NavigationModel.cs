using System;
using System.Collections.Generic;
using System.Text;
using XmlTester.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace XmlTester.UIControls.Navigation
{
    public enum CurrentScreenType { DocScreen, TestCaseScreen, RulesScreen };
    public class NavigationModel: INotifyPropertyChanged
    {
        public MainWindow MainWin { get; set; }
        public IXMLDocument CurrentDocument { get; set; }
        public ITestCase CurrentTestcase { get; set; }
        public CurrentScreenType CurrentScreen { get; set; }
        public NavigationModel(MainWindow mainWin=null)
        {
            MainWin = mainWin;
        }

        public void GoTo_Documents()
        {
            MainWin.ContentArea.Content = new DocPage(MainWin);
            CurrentScreen = CurrentScreenType.DocScreen;
            OnPropertyChanged("CurrentScreen");
            CurrentDocument = null;
            CurrentTestcase = null;
            OnPropertyChanged("CurrentTestcase");
            OnPropertyChanged("CurrentDocument");
        }

        public void GoTo_TestCases(IXMLDocument doc)
        {
            MainWin.ContentArea.Content = new TestCaseViewer(doc, MainWin);
            CurrentScreen = CurrentScreenType.TestCaseScreen;
            OnPropertyChanged("CurrentScreen");
            CurrentDocument = doc;
            OnPropertyChanged("CurrentDocument");
        }
        public void GoTo_TestCases()
        {
            MainWin.ContentArea.Content = new TestCaseViewer(CurrentDocument, MainWin);
            CurrentScreen = CurrentScreenType.TestCaseScreen;
            OnPropertyChanged("CurrentScreen");
            CurrentTestcase = null;
            OnPropertyChanged("CurrentTestcase");
        }

        public void GoTo_Rules(ITestCase testCase)
        {
            MainWin.ContentArea.Content = new RuleViewer(testCase, MainWin);
            CurrentScreen = CurrentScreenType.RulesScreen;
            OnPropertyChanged("CurrentScreen");
            CurrentTestcase = testCase;
            OnPropertyChanged("CurrentTestcase");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void OnSearchText(string val)
        {
            ISearchable page = MainWin.ContentArea.Content as ISearchable;
            if (page != null)
            {
                page.Search(val);
            }
        }


        public void BeforeSearch()
        {
            ISearchable page = MainWin.ContentArea.Content as ISearchable;
            if (page != null)
            {
                page.BeforeSearch();
            }
        }
    }
}
