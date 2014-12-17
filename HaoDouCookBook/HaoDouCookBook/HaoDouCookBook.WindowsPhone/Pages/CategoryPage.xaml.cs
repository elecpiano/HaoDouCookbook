using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.Common;

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
            rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
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
            await SearchAPI.GetCateList(UserGlobal.Instance.uuid, data =>
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
            App.CurrentInstance.RootFrame.Navigate(typeof(SearchInputPage));
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

                App.CurrentInstance.RootFrame.Navigate(typeof(CategoryTagsPage), paras);
            }
        }
        #endregion

       
    }
}
