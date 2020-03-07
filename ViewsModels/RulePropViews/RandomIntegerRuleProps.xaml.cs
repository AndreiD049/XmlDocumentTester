using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;
using XmlTesterPresentation.src;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
{
    /// <summary>
    /// Interaction logic for RandomIntegerRuleProps.xaml
    /// </summary>
    public partial class RandomIntegerRuleProps : GenericRuleProps, ITransformRuleProps, INotifyPropertyChanged
    {
        public RandomIntegerRuleProps(RandomIntegerTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)rule.Clone();
            this.View = View;
            this.View.Props = this;
            CopyPath = ((RandomIntegerTransformRule)Copy).Path;
            Min = ((RandomIntegerTransformRule)Copy).Min;
            Max = ((RandomIntegerTransformRule)Copy).Max;
            this.DataContext = this;
        }
        // TODO: rework this piece
        public int Min
        {
            get
            {
                return ((RandomIntegerTransformRule)Copy).Min;
            }
            set
            {
                if (value > Max)
                {
                    ((RandomIntegerTransformRule)Copy).Min = Max;
                    Max = value;
                    OnPropertyChanged("Max");
                }
                else
                {
                    ((RandomIntegerTransformRule)Copy).Min = value;
                }
            }
        }
        public int Max
        {
            get
            {
                return ((RandomIntegerTransformRule)Copy).Max;
            }
            set
            {
                if (value < Min)
                {
                    ((RandomIntegerTransformRule)Copy).Max = Min;
                    Min = value;
                    OnPropertyChanged("Min");
                }
                else
                {
                    ((RandomIntegerTransformRule)Copy).Max = value;
                }
            }
        }

        public string CopyPath
        {
            get
            {
                return Copy.Path;
            }
            set
            {
                Copy.Path = value;
            }
        }

        public TextBox xPath 
        {
            get
            {
                return this.Path;
            }
        }

        private void Preview_Input(object source, RoutedEventArgs e)
        {
            string text = (source as TextBox).Text;
            e.Handled = !Utils.IsNumeric(text);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
