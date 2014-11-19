using HaoDouCookBook.Common;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class RecipeCategoryDetailPage : Page
    {

        #region Field && Property
        private ObservableCollection<RecipeTileData> Recipes = new ObservableCollection<RecipeTileData>();
        #endregion

        #region Life Cycle

        public RecipeCategoryDetailPage()
        {
            this.InitializeComponent();
            this.recipesList.ItemsSource = Recipes;
            Test();
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RecipeCategoryTileData data = e.Parameter as RecipeCategoryTileData;
            if (data != null && string.IsNullOrEmpty(data.MarkText))
            {
                this.title.Text = data.Title;
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

        #region Test

        private void Test()
        {
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "12", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "0", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "888", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "343+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "豆皮是去比较大型的超市买回来的，我们这边的超市和市场好像都没有见有卖的，而且我跟老公都是爱吃豆腐皮的呢，觉得直接吃没有那么香口，所以决定加点葱丝一起炒.豆皮是去比较大型的超市买回来的，我们这边的超市和市场好像都没有见有卖的，而且我跟老公都是爱吃豆腐皮的呢，觉得直接吃没有那么香口，所以决定加点葱丝一起炒", SupportNumber = "342+", TagsText = "" });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
            Recipes.Add(new RecipeTileData() { Author = "二丫香厨", RecipeImage = Constants.DEFAULT_TOPIC_IMAGE, RecipeName = "炒西葫芦条", Recommendation = "", SupportNumber = "999+", TagsText = "午餐   夏天  身体好  家常菜   炒锅  炒   烧烤 " });
        }

        #endregion
    }
}
