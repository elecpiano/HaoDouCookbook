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
using HaoDouCookBook.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeCategoryDetailPage : BackablePage
    {
        #region Page Parameters Definition

        public class RecipeCategoryDefailPageParams
        {
            public int Id { get; set; }
            public string Title { get; set; }

            public RecipeCategoryDefailPageParams()
            {

            }
        }

        #endregion

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
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            RecipeCategoryDefailPageParams paras = e.Parameter as RecipeCategoryDefailPageParams;
            if (paras != null)
            {
                // show the bottom appbar if the title is 私人定制
                //
                if (paras.Id == 391926 || paras.Title == "私人定制")
                {
                    this.bottomAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    this.bottomAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                this.title.Text = paras.Title;
                rootScrollViewer.ScrollToVerticalOffset(0);
                Recipes.Clear();
                LoadDataAsync(0, 10, paras.Title, paras.Id);
            }

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
            var dataContext = sender.GetDataContext<RecipeTileData>(); 
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = dataContext.RecipeId;
            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        #endregion

    }
}
