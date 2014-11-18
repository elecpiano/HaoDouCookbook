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
    public sealed partial class TagsPage : Page
    {

        #region Field && Property

        private ObservableCollection<TagRecipeData> recipes = new ObservableCollection<TagRecipeData>();

        #endregion

        #region Life Cycle

        public TagsPage()
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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            this.banner.Title = e.Parameter.ToString();
            DataBinding();
            Test();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.recipeList.ItemsSource = recipes;
        }

        #endregion

        #region Event

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            App.Current.RootFrame.GoBack();
            e.Handled = true;
        }

        #endregion

        #region Test

        private void Test()
        {
            recipes.Add(new TagRecipeData() {  PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 43434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial="鸡蛋，米饭，特大号锅"});
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" }); ;
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 134, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" }); ;
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
            recipes.Add(new TagRecipeData() { PreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, LikeNumber = 13434, ViewNumber = 13413, RecipeName = "超级无敌蛋炒饭特大号000000000", FoodMaterial = "鸡蛋，米饭，特大号锅" });
        }

        #endregion

    }
}
