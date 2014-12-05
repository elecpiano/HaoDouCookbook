using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : BackablePage
    {
        #region Field && Property

        private CategoryPageViewModel viewModel = new CategoryPageViewModel();

        #endregion

        #region Life Cycle

        public CategoryPage()
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

            viewModel = new CategoryPageViewModel();
            rootScrollViewer.ScrollToVerticalOffset(0);
            DataBinding();
            LoadDataAsync();
        }

        #endregion

        #region Data Prapare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync()
        {
            await SearchAPI.GetCateList(DeviceHelper.GetUniqueDeviceID(), data =>
                {
                    if (data.Categories != null)
                    {
                        foreach (var item in data.Categories)
                        {
                            Category cate = new Category();
                            cate.Image = item.ImgUrl;
                            cate.Name = item.Cate;

                            if (item.Tags != null)
                            {
                                foreach (var tag in item.Tags)
                                {

                                    cate.Tags.Add(new CategoryTag()
                                    {
                                        Id = tag.Id,
                                        Name = tag.Name
                                    });
                                }
                            }

                            viewModel.Categories.Add(cate);
                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void Search_AppbarButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(SearchInputPage));
        }
        private void Category_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<Category>();
            if (dataContext != null)
            {
                CategoryTagsPage.CategoryTagsPageParams paras = new CategoryTagsPage.CategoryTagsPageParams();
                paras.Title = dataContext.Name;

                foreach (var item in dataContext.Tags)
                {
                    paras.Tags.Add(new CategoryTag() { 
                        Id = item.Id,
                        Name = item.Name
                    });
                }

                App.Current.RootFrame.Navigate(typeof(CategoryTagsPage), paras);
            }
        }
        #endregion

       
    }
}
