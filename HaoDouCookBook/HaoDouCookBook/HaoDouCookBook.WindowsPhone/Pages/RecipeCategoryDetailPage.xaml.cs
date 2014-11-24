using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeCategoryDetailPage : Page
    {

        #region Field && Property
        private ObservableCollection<RecipeTileData> Recipes = new ObservableCollection<RecipeTileData>();
        #endregion

        #region Life Cycle

        public RecipeCategoryDetailPage()
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
            RecipeCategoryTileData data = e.Parameter as RecipeCategoryTileData;
            if (data != null)
            {
                // show the bottom appbar if the title is 私人定制
                //
                if (data.Id == 391926 || data.Title == "私人定制")
                {
                    this.bottomAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    this.bottomAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                this.title.Text = data.Title;
                LoadDataAsync(0, 10, data.Title, data.Id);
            }

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            App.Current.RootFrame.GoBack();
            e.Handled = true;
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.recipesList.ItemsSource = Recipes;
        }

        private async Task LoadDataAsync(int offset, int limit, string typeName, int recipeId)
        {
            await RecipeAPI.GetCollectRecomment(offset, limit, null, null, null, typeName, recipeId, data =>
                {
                    foreach (var item in data.Recipes)
                    {
                        Recipes.Add(new RecipeTileData() { 
                            Author = item.UserName, 
                            TagsText = item.GetTagsString(), 
                            RecipeImage = item.Cover, 
                            RecipeName = item.Title, 
                            SupportNumber = item.LikeCount.ToString(),
                            RecipeId = item.RecipeId
                        });
                    }

                }, error =>
                {

                });
        }

        #endregion

        #region Event

        private void RecipeTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), sender.GetDataContext<RecipeTileData>().RecipeId);
        }

        #endregion

    }
}
