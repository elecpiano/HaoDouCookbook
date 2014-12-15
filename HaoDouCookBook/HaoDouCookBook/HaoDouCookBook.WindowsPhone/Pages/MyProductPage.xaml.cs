using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyProductPage : BackablePage
    {
        public ObservableCollection<UserProduct> Products { get; set; }

        public MyProductPage()
        {
            this.InitializeComponent();
            Products = new ObservableCollection<UserProduct>();
            this.prodcutsList.NoItemsText = Constants.I_DONT_HAVE_PRODUCTS;
            this.prodcutsList.LoadMoreAction = LoadMore;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            this.prodcutsList.ResetScrollViewerToBegin();
            DataBinding();
            LoadFirsPageDataAsync();
        }

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        private async Task LoadFirsPageDataAsync()
        {
            int userid = UserGlobal.Instance.GetInt32UserId();
            loading.SetState(LoadingState.LOADING);
            await RecipePhotoAPI.GetList(0, limit, userid, userid, UserGlobal.Instance.UserInfo.Sign,
               success => {
                   if(success.Products != null)
                   {
                       Products.Clear();
                       RemoveLoadMoreControl();
                       foreach (var item in success.Products)
                       {
                           Products.Add(new UserProduct() { 
                               Cover = item.PhotoUrl,
                               Id = item.RecipePhotoId
                           });
                       }

                       if(success.Products.Length == limit)
                       {
                           EnusureLoadMoreControl();
                       }
                   }

                   page = 1;
                   loading.SetState(LoadingState.SUCCESS);

               }, error => {
                   Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirsPageDataAsync());
               });
        }


        #region Load More

        int page = 1;
        int limit = 21;

        private UserProduct loadMoreControlDataContext = new UserProduct() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (Products != null && !Products.Contains(loadMoreControlDataContext))
            {
                Products.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (Products != null && Products.Contains(loadMoreControlDataContext))
            {
                Products.Remove(loadMoreControlDataContext);
            }
        }

        private async void LoadMore(LoadMoreControl loadmore)
        {
            int uid = UserGlobal.Instance.GetInt32UserId();
            loadmore.SetState(LoadingState.LOADING);
            await RecipePhotoAPI.GetList(
                page * limit,
                limit,
                uid,
                uid,
                UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    if (success.Products != null)
                    {
                        RemoveLoadMoreControl();
                        foreach (var item in success.Products)
                        {
                            Products.Add(new UserProduct()
                            {
                                Cover = item.PhotoUrl,
                                Id = item.RecipePhotoId
                            });
                        }

                        if (success.Products.Length == limit)
                        {
                            EnusureLoadMoreControl();
                        }
                    }
                    else
                    {
                        loadmore.SetState(LoadingState.DONE);
                    }
                },
               error =>
               {
                   Utilities.CommondLoadMoreErrorBehavoir(loadmore, error);
               });
        }

        #endregion

    }
}
