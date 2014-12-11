using HaoDouCookBook.Common;
using HaoDouCookBook.ViewModels;
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
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PublishRecipePage : Page
    {
        #region Field && Proprety

        private PublishRecipePageViewModel viewModel = new PublishRecipePageViewModel();
        
        #endregion

        #region Life Cycle

        public PublishRecipePage()
        {
            this.InitializeComponent();
            DataBinding();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            base.OnNavigatedFrom(e);
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        #endregion

        #region Event

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {

        }

        private void Clear_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtBegin<StuffItem>(sender, false);
        }

        private void AddMainStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.MainStuffs.Add(new StuffItem());
            }
        }

        private void AddOtherStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.OtherStuffs.Add(new StuffItem());
            }
        }

        private void RemoveMainStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<StuffItem>();
            viewModel.MainStuffs.Remove(dataContext);
        }

        private void RemoveOtherStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<StuffItem>();
            viewModel.OtherStuffs.Remove(dataContext);
        }

        #endregion
    }
}
