using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
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
using HaoDouCookBook.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class FavoriteRecipes : UserControl
    {
        #region Field && Property

        private FavoriteRecipesViewModel viewModel = new FavoriteRecipesViewModel();

        #endregion

        #region Life Cycle

        public FavoriteRecipes()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods

        public async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await FavoriteAPI.GetMyAlbum(0, 20, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.uuid,
                data => 
                {
                    if(data.Albums != null)
                    {
                        viewModel = new FavoriteRecipesViewModel();
                        DataBinding();

                        foreach (var item in data.Albums)
                        {
                            if (item.Sys == 0 && item.Title.Equals("我喜欢的菜谱"))
                            {
                                viewModel.MyLikeRecipesAlbum.AlbumId = item.Cid;
                                viewModel.MyLikeRecipesAlbum.AlbumName = item.Title;
                                viewModel.MyLikeRecipesAlbum.RecipeNumber = item.RecipeCount;
                            }
                            else if (item.Sys == 1)
                            {
                                viewModel.MyFavoriteRecipesAlbum.AlbumId = item.Cid;
                                viewModel.MyFavoriteRecipesAlbum.AlbumName = item.Title;
                                viewModel.MyFavoriteRecipesAlbum.RecipeNumber = item.RecipeCount;
                            }
                            else
                            {
                                viewModel.AlbumsCreatedByMe.Add(new RecipesAblum() {
                                    AlbumId = item.Cid,
                                    AlbumName = item.Title,
                                    Cover = item.Avatar,
                                    RecipeNumber = item.RecipeCount
                                });
                            }
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
                    else
                    {
                        loading.SetState(LoadingState.DONE);
                    }
                });
        }

        #endregion

        #region Private Methods

        private void DataBinding()
        {
            this.root.DataContext = viewModel; 
        }

        #endregion

        private void RecipeAlbum_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipesAblum>();

            FavoriteRecipeAlbumPage.FavoriteRecipeAlbumPageParams paras = new FavoriteRecipeAlbumPage.FavoriteRecipeAlbumPageParams();
            paras.AlbumId = dataContext.AlbumId;
            paras.Title = dataContext.AlbumName;

            App.Current.RootFrame.Navigate(typeof(FavoriteRecipeAlbumPage), paras);

        }

        #region Event

        #endregion
    }
}
