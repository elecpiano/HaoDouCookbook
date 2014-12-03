using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using HaoDouCookBook.Pages;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
            if(data.Actors != null)
            {
                foreach (var item in data.Actors)
                {
                    viewModel.Masters.Add(new CookMaster() {
                        CookMasterImageSource = item.ActImg,
                        OpenUrl = item.OpenUrl
                    });
                }

            }


            //// Tutorial
            ////
            //viewModel.Tutorial.Title = data.NewbieTutorial.Title;
            //viewModel.Tutorial.Teacher.Name = data.NewbieTutorial.UserName;
            //viewModel.Tutorial.Teacher.UserPhoto = data.NewbieTutorial.UserAvatar;
            //viewModel.Tutorial.MainImageSource = data.NewbieTutorial.RecipeCover;
            //viewModel.Tutorial.OpenUrl = data.NewbieTutorial.OpenUrl;

            //viewModel.Tutorial.DetailsImageSources.Clear();
            //foreach (var item in data.NewbieTutorial.SamllCovers)
            //{
            //    viewModel.Tutorial.DetailsImageSources.Add(item);
            //}
            //viewModel.Tutorial.Teacher.ArchiveDescription = data.NewbieTutorial.Intro;

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

        #region Event

        private void Meal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var meal = sender.GetDataContext<ViewModels.Meal>();

            ProductPage.ProductPageParams paras = new ProductPage.ProductPageParams();
            paras.ProductId = meal.ProductId;
            paras.TopicId = meal.Id;
            paras.Type = 1;

            App.Current.RootFrame.Navigate(typeof(ProductPage), paras);
        }

        private void Tutorial_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TutorialViewPage.TutorialViewPageParams paras = new TutorialViewPage.TutorialViewPageParams();
            paras.Url = "http://m.haodou.com/app/recipe/act/novice.php";

            App.Current.RootFrame.Navigate(typeof(TutorialViewPage), paras);
        }

        private void Cate_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Cate cate = sender as Cate;
            ViewModels.Cate dataContext = sender.GetDataContext<ViewModels.Cate>();
            int index = viewModel.Cates.IndexOf(dataContext);

            if (cate != null)
            {
                cate.AdjustLayout(index);
            }
        }

        #endregion

        
    }
}
