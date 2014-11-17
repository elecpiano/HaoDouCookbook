﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class SimpleSettingItem : UserControl
    {

        #region Denpendency Property

        public static readonly DependencyProperty WithMoreTagProperty = DependencyProperty.Register("WithMoreTag", typeof(bool), typeof(SimpleSettingItem), new PropertyMetadata(true));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(SimpleSettingItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ItemTextProperty = DependencyProperty.Register("ItemText", typeof(string), typeof(SimpleSettingItem), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Property Wrapper

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string ItemText
        {
            get { return (string)GetValue(ItemTextProperty); }
            set { SetValue(ItemTextProperty, value); }
        }

        public bool WithMoreTag
        {
            get { return (bool)GetValue(WithMoreTagProperty); }
            set { SetValue(WithMoreTagProperty, value); }
        }

        #endregion

        #region Life Cycle

        public SimpleSettingItem()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}