using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Shared.Utility;
using HaoDouCookBook.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class FavoriteAlbums : UserControl
    {
        #region Field && Proprety

        public ObservableCollection<FavoriteAlbumItem> Recipes { get; set; } 

        #endregion

        #region Life Cycle

        public FavoriteAlbums()
        {
            this.InitializeComponent();
            Recipes = new ObservableCollection<FavoriteAlbumItem>();
            DataBinding();
        }

        #endregion

        #region Public Methods

        public async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await FavoriteAPI.GetCollectList(0, 21, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.uuid, data =>
                {
                    Recipes.Clear();

                    if(data.Recipes != null)
                    {
                        foreach (var item in data.Recipes)
                        {
                            Recipes.Add(new FavoriteAlbumItem() { 
                                Cover = item.Cover,
                                Intro = item.Contents,
                                AlbumId = item.Cid,
                                Title = item.Title
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);
                },
                error => 
                {
                    if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                    {
                        loading.RetryAction = async () => await LoadFirstPageDataAsync();
                        loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                    }
 
                });
        }

        #endregion

        #region Private Methods

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteAlbumItem>();
            AlbumPage.AlbumPageParams paras = new AlbumPage.AlbumPageParams();
            paras.AlbumId = dataContext.AlbumId;

            App.Current.RootFrame.Navigate(typeof(AlbumPage), paras);
        }

        #endregion
    }
}
