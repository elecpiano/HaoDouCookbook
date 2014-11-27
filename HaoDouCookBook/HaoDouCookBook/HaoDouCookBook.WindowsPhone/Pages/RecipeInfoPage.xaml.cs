﻿using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
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
using ViewModels = HaoDouCookBook.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeInfoPage : BackablePage
    {
        #region Field && Property

        private RecipeInfoPageViewModel viewModel = new RecipeInfoPageViewModel();

        #endregion

        #region Life Cycle

        public RecipeInfoPage()
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

            viewModel = new RecipeInfoPageViewModel();
            rootScrollViewer.ScrollToVerticalOffset(0);
            int recipeId;

            if (Int32.TryParse(e.Parameter.ToString(), out recipeId))
            {
                LoadDataAsync(string.Empty, null, recipeId);
            }

        }
        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(string sign, int? uid, int recipeId)
        {
            await InfoAPI.GetInfo(sign, uid, DeviceHelper.GetUniqueDeviceID(), recipeId, data =>
                {
                    UpdateViewModel(data);
                }, error => { });
        }

        private void UpdateViewModel(InfoPageData data)
        {
            DataBinding();

            var infoData = data.Info;

            if (infoData == null)
            {
                return;
            }

            viewModel.RecipeId = infoData.RecipeId;
            viewModel.Cover = infoData.Cover;
            viewModel.Title = infoData.Title;
            viewModel.CookTime = infoData.CookTime;
            viewModel.ReadyTime = infoData.ReadyTime;
            viewModel.Tips = infoData.Tips;
            viewModel.UserName = infoData.UserName;
            viewModel.FavoriteCount = infoData.FavoriteCount;
            viewModel.PhotoCount = infoData.PhotoCount;
            viewModel.ReviewTime = infoData.ReviewTime;
            viewModel.UserId = infoData.UserId;
            viewModel.Avatar = infoData.Avatar;
            viewModel.ViewCount = infoData.ViewCount;
            viewModel.Type = infoData.Type;
            viewModel.UserCount = infoData.UserCount;
            viewModel.LikeCount = infoData.LikeCount;
            viewModel.IsLike = infoData.IsLike == 1 ? true : false;
            viewModel.AdFlag = infoData.AdFlag;
            viewModel.ProductCount = infoData.ProductCount;
            viewModel.IsVip = infoData.Vip == 1 ? true : false;

            // Fodd stuff
            //
            if (infoData.Stuff != null)
            {
                foreach (var item in infoData.Stuff)
                {
                    ViewModels.FoodStuff stuff = new ViewModels.FoodStuff();
                    stuff.Category = item.Category;
                    stuff.CategoryId = item.CategoryId;
                    stuff.Id = item.Id;
                    stuff.Name = item.Name;
                    stuff.Type = item.Type;
                    stuff.Weight = item.Weight;
                    stuff.FoodFlag = item.FoodFlag == 1 ? true : false;

                    if (infoData.MainStuff != null)
                    {
                        stuff.IsMainStuff = infoData.MainStuff.Any(s => s.Id == item.Id);
                    }

                    viewModel.Stuffs.Add(stuff);
                }

            }

            // Cook setps
            //
            if (infoData.Steps != null)
            {
                for (int i = 0; i < infoData.Steps.Length; i++)
                {
                    var stepData = infoData.Steps[i];
                    viewModel.CookSteps.Add(new ViewModels.CookStep()
                    {
                        Intro = stepData.Intro,
                        Photo = stepData.StepPhoto,
                        StepNumber = i + 1
                    });

                }
            }

            // ad
            //
            viewModel.Ad.Image = infoData.AD.Image;
            viewModel.Ad.Title = infoData.AD.Title;
            viewModel.Ad.Url = infoData.AD.Url;

            // Tgs
            //
            if (infoData.Tags != null)
            {
                foreach (var item in infoData.Tags)
                {
                    viewModel.Tags.Add(new TagItem() { Id = item.Id, Text = item.Name.Trim() });
                }
            }

            // Pohto List
            //
            if (infoData.PhotoList != null)
            {
                foreach (var item in infoData.PhotoList)
                {
                    viewModel.PhotoList.Add(item);
                }
            }

            // comments
            //
            if (infoData.CommentList != null)
            {
                foreach (var item in infoData.CommentList)
                {
                    viewModel.Comments.Add(new ViewModels.Comment() {
                        AtUserId = item.AtUserId,
                        AtUserName =item.AtUserName,
                        UserId = item.UserId,
                        UserName =item.UserName,
                        Avatar = item.Avatar,
                        Content = item.Content,
                        CreateTime = item.CreateTime
                    });
                }
            }

            // Products
            //
            if (infoData.Product != null)
            {
                foreach (var item in infoData.Product)
                {
                    viewModel.Products.Add(new ViewModels.Product() {
                         Content = item.Content,
                         UserId = item.UserId,
                         UserName = item.UserName,
                         Image = item.Image
                    });
                    
                }
            }
        }

        #endregion

        #region Event

       

        #endregion
    }
}
