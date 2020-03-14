using System.Windows.Controls;
using System.Windows;
using XmlTester.Interfaces;
using XmlTester.src.TransformRules;
using XmlTester.src;
using System.Xml;

namespace XmlTester.ViewsModels.RulePropViews
{
    /// <summary>
    /// Interaction logic for IncrementStringRuleProps.xaml
    /// </summary>
    public partial class IncrementStringRuleProps : GenericRuleProps, ITransformRuleProps 
    {
        public IncrementStringRuleProps(IncrementStringTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)Rule.Clone();
            this.View = View;
            this.View.Props = this;
            this.DataContext = Copy;
        }
        public void Update()
        {
            TreeView tree = View.docTreeViewControl;
            if (tree.SelectedItem != null)
            {
                ITreeElement selected_item = tree.SelectedItem as ITreeElement;
                this.Path.Text = Utils.getFullPath(selected_item.Node);
                this.CurVal.Text = selected_item.Node.InnerText;
            }
        }
        public new void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Path.Text = "";
            base.Duplicate_Clicked(sender, e);
        }
    }
}
