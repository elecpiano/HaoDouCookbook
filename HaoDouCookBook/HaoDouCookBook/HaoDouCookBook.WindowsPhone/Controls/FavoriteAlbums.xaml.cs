using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Shared.Utility;
using HaoDouCookBook.Pages;
using System;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class FavoriteAlbums : UserControl
    {
        #region Field && Proprety

        public ObservableCollection<FavoriteAlbumItem> Recipes { get; set; }

        public Action<string> DeleteSingleSuccessAction { get; set; }

        public Action<Error> DeleteSingleFailAction { get; set; }

        public Action<string> DeleteAllSuccessAction { get; set; }

        public Action<Error> DeleteAllFailAction { get; set; }
        #endregion

        #region Life Cycle

        public FavoriteAlbums()
        {
            this.InitializeComponent();
            Recipes = new ObservableCollection<FavoriteAlbumItem>();
            DataBinding();
            this.noItemsText.Text = Constants.NO_FAVORITE_ALUMBS;
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
                    Utilities.CommonLoadingRetry(loading, error, async() => await LoadFirstPageDataAsync());
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

        private async void AllDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteAlbumItem>();

            await Utilities.ShowOKCancelDialog("提示", "您确定要删除吗？", async () =>
                {
                    await FavoriteAPI.Del(UserGlobal.Instance.GetInt32UserId(), 2, dataContext.AlbumId.ToString(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, true,
                        async success =>
                        {
                            if (DeleteAllSuccessAction != null)
                            {
                                DeleteAllSuccessAction.Invoke(success.Message);
                            }
                            await LoadFirstPageDataAsync();
                        },
                        error =>
                        {
                            if (DeleteAllFailAction != null)
                            {
                                DeleteAllFailAction.Invoke(error);
                            }
                        });
                }, null);
        }

        private async void Delete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteAlbumItem>();

            await Utilities.ShowOKCancelDialog("提示", "您确定要删除吗？", async () =>
                {
                    await FavoriteAPI.Del(UserGlobal.Instance.GetInt32UserId(), 2, dataContext.AlbumId.ToString(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, false,
                        async success => {
                            if (DeleteSingleSuccessAction != null)
                            {
                                DeleteSingleSuccessAction.Invoke(success.Message);
                            }
                            await LoadFirstPageDataAsync();
                        },
                        error => {
                            if (DeleteSingleFailAction != null)
                            {
                                DeleteSingleFailAction.Invoke(error);
                            }
                        });
                }, null);
        }

        private void Album_Hoding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            sender.ShowFlayout();
        }

        #endregion
    }
}
