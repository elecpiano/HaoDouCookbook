﻿using HaoDouCookBook.Common;
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
using HaoDouCookBook.HaoDou.DataModels.My;

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
                    UpdateData(data);
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

        private void UpdateData(FavoriteAlbumsData data)
        {
            if (data.Albums != null)
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
                        viewModel.AlbumsCreatedByMe.Add(new RecipesAblum()
                        {
                            AlbumId = item.Cid,
                            AlbumName = item.Title,
                            Cover = item.Avatar,
                            RecipeNumber = item.RecipeCount
                        });
                    }
                }
            }
        }

        private void DataBinding()
        {
            this.root.DataContext = viewModel; 
        }

        private async void CreateNewAlbum(string newAlbumName)
        {
            if (string.IsNullOrEmpty(newAlbumName))
            {
                toast.Show("分类名不能为空");
                return;
            }

            await FavoriteAPI.AddMyAlbum(UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, newAlbumName,  async success =>
                {
                    toast.Show(success.Message);
                    await LoadFirstPageDataAsync();

                }, error => {
                    toast.Show(error.Message);
                });
        }

        private async void RenameAlbum(int albumId, string newAlbumName)
        {
            if (string.IsNullOrEmpty(newAlbumName))
            {
                toast.Show("分类名不能为空");
            }

            await FavoriteAPI.RenameMyAlbum(albumId, UserGlobal.Instance.GetInt32UserId(), newAlbumName, UserGlobal.Instance.UserInfo.Sign,
                async success =>
                {
                    toast.Show(success.Message);
                    await LoadFirstPageDataAsync(); 
                },
                error =>
                {
                    toast.Show(error.Message);
                });
        }

        #endregion

        #region Event

        private void RecipeAlbum_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipesAblum>();

            FavoriteRecipeAlbumPage.FavoriteRecipeAlbumPageParams paras = new FavoriteRecipeAlbumPage.FavoriteRecipeAlbumPageParams();
            paras.AlbumId = dataContext.AlbumId;
            paras.Title = dataContext.AlbumName;

            App.Current.RootFrame.Navigate(typeof(FavoriteRecipeAlbumPage), paras);

        }

        private void CreateNew_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
            paras.MaxLength = 20;
            paras.PlaceholderText = "输入分类名称，最多20个字符";
            paras.ConfirmAction = CreateNewAlbum;

            App.Current.RootFrame.Navigate(typeof(BigTextBox), paras);
        }

        private async void DeleteAlbum_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipesAblum>();

            ContentDialog dialog = new ContentDialog()
            {
                Title = "温馨提示",
                Content = string.Format("您确定要删除专辑[{0}]吗？", dataContext.AlbumName),
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };

            var result = await dialog.ShowAsync();

            if(result == ContentDialogResult.Primary)
            {
                await FavoriteAPI.DeleteMyAlbum(
                    UserGlobal.Instance.GetInt32UserId(),
                    dataContext.AlbumId,
                    UserGlobal.Instance.UserInfo.Sign,
                    UserGlobal.Instance.uuid,
                    async success => {
                        toast.Show(success.Message);
                        await LoadFirstPageDataAsync();
                    },
                    error => {
                        toast.Show(error.Message);
                    });
            }
        }

        private void SmartGrid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            if (senderElement == null)
            {
                return;
            }

            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            if (flyoutBase != null)
            {
                flyoutBase.ShowAt(senderElement);
            }
        }

        private void EditAlbum_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipesAblum>();
            BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
            paras.MaxLength = 20;
            paras.Text = dataContext.AlbumName;
            paras.ConfirmAction = (newAlbumName) =>
                {
                    RenameAlbum(dataContext.AlbumId, newAlbumName);
                };

            App.Current.RootFrame.Navigate(typeof(BigTextBox), paras);
        }

        #endregion
    }
}
