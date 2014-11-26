using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using HaoDouCookBook.Pages;
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
using ViewModels = HaoDouCookBook.ViewModels;
using Shared.Utility;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class DiscoveryPageContent : UserControl
    {
        #region Field && Property

        private ViewModels.DiscoveryPageViewModel viewModel = new DiscoveryPageViewModel();

        private Random random = new Random();

        #endregion

        #region Life Cycle

        public DiscoveryPageContent()
        {
            this.InitializeComponent();
            DataBinding();
            this.Loaded += DiscoveryPageContent_Loaded;
            
        }

        void DiscoveryPageContent_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync()
        {
            await RecipeAPI.GetDiscoveryData(data =>
                {
                    UpdatePageData(data);

                }, error =>
                {

                });
        }


        private void UpdatePageData(DiscoveryPageData data)
        {
            if (data == null)
            {
                return;
            }

            // Meals
            //
            if (data.DailyMeal != null)
            {
                foreach (var item in data.DailyMeal.Meals)
                {
                    viewModel.DailyMeals.Add(new ViewModels.Meal()
                    {
                        MealImageSource = item.ThemeCover,
                        Number = item.PhotoCount,
                        Title = item.ThemeTitle,
                        Id = item.TopicId,
                        ProductId = item.Pid
                    });

                }
            }

            // Actor
            //
            viewModel.Master.CookMasterImageSource = data.Actor.ActImg;


            // Tutorial
            //
            viewModel.Tutorial.Title = data.NewbieTutorial.Title;
            viewModel.Tutorial.Teacher.Name = data.NewbieTutorial.UserName;
            viewModel.Tutorial.Teacher.UserPhoto = data.NewbieTutorial.UserAvatar;
            viewModel.Tutorial.MainImageSource = data.NewbieTutorial.RecipeCover;

            viewModel.Tutorial.DetailsImageSources.Clear();
            foreach (var item in data.NewbieTutorial.SamllCovers)
            {
                viewModel.Tutorial.DetailsImageSources.Add(item);
            }
            viewModel.Tutorial.Teacher.ArchiveDescription = data.NewbieTutorial.Intro;

            //cates
            //
            if (data.Cates != null)
            {
                foreach (var dCate in data.Cates)
                {
                    ViewModels.Cate vcate = new ViewModels.Cate();
                    vcate.Title = dCate.Title;
                    if (dCate.Items != null)
                    {
                        foreach (var item in dCate.Items)
                        {
                            vcate.Dishes.Add(new DishTileData() {
                                Author = item.UserName,
                                DishImageSource = item.Cover,
                                SupportNumber = item.Count,
                                DishImageWidth = 175,
                                Id = dCate.TopicId,
                                ProductId = item.Id
                            });
                        }
                    }

                    for (int i = 0; i < vcate.Dishes.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            vcate.Dishes[i].DishImageHeight = random.Next(160, 220);
                        }
                        else
                        {
                            vcate.Dishes[i].DishImageHeight = 360 - vcate.Dishes[i - 1].DishImageHeight;
                        }
                    }

                    viewModel.Cates.Add(vcate);
                }
            }

            // Starred Users
            //
            viewModel.StarredUsers.Clear();
            foreach (var user in data.StarredUser.Users)
            {
                viewModel.StarredUsers.Add(new UserData() { UserPhoto = user.Avatar, Name = user.UserName });
            }

        }


        #endregion

        #region LayoutAdjust

        private void RandomDishTileGroupImageHeight(DishTile tileOne, DishTile tileTwo)
        {
            double totalHeight = 360;
            double itemOneHeight = random.Next(160, 220);
            double itemTwoHeight = totalHeight - itemOneHeight;

            tileOne.SetImageHight(itemOneHeight);
            tileTwo.SetImageHight(itemTwoHeight);
        }

        #endregion

        private void Meal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(ProductPage), sender.GetDataContext<ViewModels.Meal>());
        }

        private void cateItems_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(ProductPage), sender.GetDataContext<DishTileData>());
        }

    }
}
