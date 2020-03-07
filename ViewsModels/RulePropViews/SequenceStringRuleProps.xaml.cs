using System.Windows.Controls;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src.TransformRules;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace XmlTesterPresentation.ViewsModels.RulePropViews
{
    /// <summary>
    /// Interaction logic for SequenceStringRuleProps.xaml
    /// </summary>
    public partial class SequenceStringRuleProps : GenericRuleProps, ITransformRuleProps 
    {
        private bool indexChanged = false;
        public ObservableCollection<string> ObservableValues { get; set; }
        public SequenceStringRuleProps(SequenceTransformRule rule, RuleViewer View)
        {
            InitializeComponent();
            Rule = rule;
            Copy = (IXMLTransformRule)rule.Clone();
            this.View = View;
            this.View.Props = this;
            this.DataContext = Copy;
            ObservableValues = new ObservableCollection<string>(((SequenceTransformRule)Copy).Values);
            ListItems.ItemsSource = ObservableValues; 
        }

        public void Update()
        {
            TreeView tree = View.docTreeViewControl;
            if (tree.SelectedItem != null)
            {
                NodeTreeViewItem selected_item = tree.SelectedItem as NodeTreeViewItem;
                this.Path.Text = selected_item.FullPath;
            }
            NextValue.Text = ((SequenceTransformRule)Copy).NextValueItem;
            NextIndex.Text = ((SequenceTransformRule)Copy).NextValue.ToString();

        }
        public new void Duplicate_Clicked(object sender, RoutedEventArgs e)
        {
            Path.Text = "";
            base.Duplicate_Clicked(sender, e);
        }

        private void Add_Clicked(object source, RoutedEventArgs e)
        {
            if (NewVal.Text != string.Empty)
            {
                ObservableValues.Add(NewVal.Text);
                ((SequenceTransformRule)Copy).Values.Add(NewVal.Text);
                NewVal.Clear();
                Update();
            }
        }

        private void Add_Enter_Pressed(object source, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Add_Clicked(source, e);
            }
       
        }

        private void RemoveValue(int idx)
        {
            ObservableValues.RemoveAt(idx);
            ((SequenceTransformRule)Copy).Values.RemoveAt(idx);
            Update();
        }

        private void Remove_Clicked(object source, RoutedEventArgs e)
        {
            if (ListItems.SelectedItem != null)
            {
                int selected_idx = ListItems.SelectedIndex;
                RemoveValue(selected_idx);
            }
        }

        private void Move_Up(object source, RoutedEventArgs e)
        {
            int selected_idx = ListItems.SelectedIndex;
            int to_idx = selected_idx - 1;
            if (to_idx >= 0)
            {
                ObservableValues.Move(selected_idx, to_idx);
                ((SequenceTransformRule)Copy).Move(selected_idx, to_idx);
            }
            Update();
        }
        private void Move_Down(object source, RoutedEventArgs e)
        {
            int selected_idx = ListItems.SelectedIndex;
            int to_idx = selected_idx + 1;
            if (to_idx < ObservableValues.Count)
            {
                ObservableValues.Move(selected_idx, to_idx);
                ((SequenceTransformRule)Copy).Move(selected_idx, to_idx);
            }
            Update();
        }

        private void Index_Move_Up(object source, RoutedEventArgs e)
        {
            SequenceTransformRule rule = Copy as SequenceTransformRule;
            if (rule.NextValue > 0)
            {
                rule.NextValue -= 1;
                indexChanged = true;
                Update();
            }
        }
        private void Index_Move_Down(object source, RoutedEventArgs e)
        { 
            SequenceTransformRule rule = Copy as SequenceTransformRule;
            if (rule.NextValue < rule.Values.Count - 1)
            {
                rule.NextValue += 1;
                indexChanged = true;
                Update();
            }
        }

        private new void Save_Clicked(object source, RoutedEventArgs e)
        {
            if (indexChanged)
            {
                SetPrevNextValue();
            }
            base.Save_Clicked(source, e);
        }
        /// <summary>
        /// Needed when we manually set the next item in sequence.
        /// </summary>
        private void SetPrevNextValue()
        {
            SequenceTransformRule rule = Copy as SequenceTransformRule;
            if (rule.NextValue == 0)
                rule.NextValue = rule.Values.Count - 1;
            else
                rule.NextValue -= 1;
        }
    }
}
