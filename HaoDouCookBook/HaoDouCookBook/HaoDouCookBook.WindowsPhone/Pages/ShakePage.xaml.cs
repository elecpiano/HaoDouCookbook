using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using HaoDouCookBook.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShakePage : BackablePage
    {
        #region Field && Property

        private ShakePageViewModel viewModel = new ShakePageViewModel();


        #endregion

        #region Life Cycle

        public ShakePage()
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
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            viewModel.Recipes.Clear();
            LoadDataAsync(string.Empty, null);
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(string sign, int? uid)
        {
            await InfoAPI.Shake(sign, uid, DeviceHelper.GetUniqueDeviceID(), data =>
                {
                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            viewModel.Recipes.Add(new TagRecipeData()
                            {
                                FoodStuff = item.Stuff,
                                LikeNumber = item.LikeCount,
                                PreviewImageSource = item.Cover,
                                ViewNumber = item.ViewCount,
                                RecipeName = item.Title,
                                RecipeId = item.RecipeId
                            });

                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = sender.GetDataContext<TagRecipeData>().RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        #endregion
    }
}
