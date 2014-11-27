﻿using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
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
    public sealed partial class TagsPage : BackablePage
    {

        #region Field && Property

        private ObservableCollection<TagRecipeData> recipes = new ObservableCollection<TagRecipeData>();

        #endregion

        #region Life Cycle

        public TagsPage()
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

            TagItem tagCategoryItem = e.Parameter as TagItem;

            if (tagCategoryItem != null)
            {
                recipes.Clear();
                rootScrollViewer.ScrollToVerticalOffset(0);
                this.title.Text = tagCategoryItem.Text;
                LoadDataAsync(0, 10, tagCategoryItem.Id, tagCategoryItem.Text);

            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.recipeList.ItemsSource = recipes;
        }

        private async Task LoadDataAsync(int offset, int limit, int? tagid, string keyword)
        {
            await SearchAPI.GetList(offset, limit, DeviceHelper.GetUniqueDeviceID(), tagid, keyword, data =>
                {
                    DataBinding();

                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            recipes.Add(new TagRecipeData() { 
                                FoodStuff = item.Stuff, 
                                LikeNumber = item.LikeCount, 
                                ViewNumber = item.ViewCount, 
                                PreviewImageSource = item.Cover, 
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
            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), sender.GetDataContext<TagRecipeData>().RecipeId);
        }

        #endregion

    }
}
