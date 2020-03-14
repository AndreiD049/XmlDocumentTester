using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules;
using XmlTester.src;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XmlTester.ViewsModels.RulePropViews
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
            this.DataContext = Copy;
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

        private new void Save_Clicked(object sender, RoutedEventArgs e)
        {
            if (!Copy.Validator.Validate())
            {
                MessageBoxResult answer = MessageBox.Show("Cannot Save rule. Min is greater than Max. Do you want to swap them?",
                                                          "Warning",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Warning);
                if (answer == MessageBoxResult.Yes)
                {
                    RandomIntegerTransformRule rule = Copy as RandomIntegerTransformRule;
                    int temp = rule.Min;
                    rule.Min = rule.Max;
                    rule.Max = temp;
                }
                else
                {
                    return;
                }
            }
            base.Save_Clicked(sender, e);
        }
        public void Update()
        {
            TreeView tree = View.docTreeViewControl;
            if (tree.SelectedItem != null)
            {
                ITreeElement selected_item = tree.SelectedItem as ITreeElement;
                this.Path.Text = Utils.getFullPath(selected_item.Node);
            }
        }

        public new void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Path.Text = "";
            base.Duplicate_Clicked(sender, e);
        }
    }
}
