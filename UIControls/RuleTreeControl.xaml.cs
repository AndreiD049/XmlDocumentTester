﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using XmlTesterPresentation.Views;
using XmlTesterPresentation.Interfaces;
using XmlTesterPresentation.src;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XmlTesterPresentation.UIControls
{
    /// <summary>
    /// Interaction logic for RuleTreeControl.xaml
    /// </summary>
    public partial class RuleTreeControl : UserControl
    {
        public RuleViewer View { get; set; }
        public RuleViewModel ViewModel { get; set; }
        public RuleTreeControl()
        {
            InitializeComponent();
        }

        private void ListViewScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}