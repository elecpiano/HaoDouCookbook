using HaoDouCookBook.Controls;
using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BigTextBox : BackablePage
    {
        #region Field && Property

        public static string Text = string.Empty;

        #endregion

        #region Page Paremeter Definition

        public class BigTextBoxParams
        {
            public string Text { get; set; }

            public string PlaceholderText { get; set; }

            public string Description { get; set; }

            public int MaxLength { get; set; }

            /// <summary>
            /// Callback action after ok appbarbutton click
            /// </summary>
            public Action<string> ConfirmAction { get; set; }

            public BigTextBoxParams()
            {
                MaxLength = 4096;
                Text = string.Empty;
                PlaceholderText = string.Empty;
                Description = string.Empty;
            }
        }

        #endregion

        #region Field && Property

        private BigTextBoxParams pageParams;

        #endregion

        #region Life Cycle

        public BigTextBox()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            pageParams = e.Parameter as BigTextBoxParams;
            if (pageParams != null)
            {
                if (!string.IsNullOrEmpty(pageParams.Text))
                {
                    this.textBox.Text = pageParams.Text; 
                }

                this.textBox.PlaceholderText = pageParams.PlaceholderText;
                this.textBox.MaxLength = pageParams.MaxLength;
                this.textBox.Text = pageParams.Text;
                this.desc.Text = pageParams.Description;
            }
        }

        #endregion

        #region Event

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (pageParams.ConfirmAction != null)
            {
                string text = this.textBox.Text;
                pageParams.ConfirmAction.Invoke(text);
            }

            App.Current.RootFrame.GoBack();
        }

        #endregion
    }
}
