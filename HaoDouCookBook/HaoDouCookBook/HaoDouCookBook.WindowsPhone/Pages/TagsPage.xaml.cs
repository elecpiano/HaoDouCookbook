using HaoDouCookBook.Common;
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
            TagItem tagCategoryItem = e.Parameter as TagItem;

            if (tagCategoryItem != null)
            {
                this.title.Text = tagCategoryItem.Text;
                LoadDataAsync(0, 10, tagCategoryItem.Id, tagCategoryItem.Text);

            }
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

        private async Task LoadDataAsync(int offset, int limit, int tagid, string keyword)
        {
            await SearchAPI.GetList(offset, limit, DeviceHelper.GetUniqueDeviceID(), tagid, keyword, data =>
                {
                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            recipes.Add(new TagRecipeData() { 
                                FoodMaterial = item.Stuff, 
                                LikeNumber = item.LikeCount, 
                                ViewNumber = item.ViewCount, 
                                PreviewImageSource = item.Cover, 
                                RecipeName = item.Title 
                            });
                        }

                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            App.Current.RootFrame.GoBack();
            e.Handled = true;
        }

        #endregion

    }
}
