using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.DiscoveryPage;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class DiscoveryPageContent : UserControl
    {
        #region Field && Property

        private ObservableCollection<DishTileData> cateOneDishes = new ObservableCollection<DishTileData>(); //例如煲一锅幸福爆棚的汤
        private ObservableCollection<DishTileData> cateTwoDishes = new ObservableCollection<DishTileData>(); //例如晒一晒
        private ObservableCollection<UserData> usersData = new ObservableCollection<UserData>();

        private ObservableCollection<HaoDouCookBook.ViewModels.Meal> dailyMeals = new ObservableCollection<HaoDouCookBook.ViewModels.Meal>();
        private CookMaster cookMaster = new CookMaster();
        private HaoDouCookBook.ViewModels.NewbieTutorial newbieTutorial = new HaoDouCookBook.ViewModels.NewbieTutorial();

        private Random random = new Random();

        #endregion

        #region Life Cycle

        public DiscoveryPageContent()
        {
            this.InitializeComponent();
            DataBinding();
            LoadDataAsync();
            AdjustDishTiles();
            this.Loaded += DiscoveryPageContent_Loaded;
        }

        void DiscoveryPageContent_Loaded(object sender, RoutedEventArgs e)
        {
            Test();
            
            
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            BindDishes();
            BindMeales();
            BindCookMaster();
            BindUserData();
            BindNewbieTutorial();
        }

        private void BindUserData()
        {
            this.userMasterList.ItemsSource = usersData;
        }

        private void BindMeales()
        {
            dailyMeals.Add(new HaoDouCookBook.ViewModels.Meal());
            dailyMeals.Add(new HaoDouCookBook.ViewModels.Meal());
            dailyMeals.Add(new HaoDouCookBook.ViewModels.Meal());
            this.mealLeft.DataContext = dailyMeals[0];
            this.mealRightTop.DataContext = dailyMeals[1];
            this.mealRightBottom.DataContext = dailyMeals[2];
        }

        private void BindCookMaster()
        {
            this.cookMasterImage.DataContext = cookMaster;
        }

        private void BindDishes()
        {
            for (int i = 0; i < 4; i++)
            {
                cateOneDishes.Add(new DishTileData());
                cateTwoDishes.Add(new DishTileData());
            }
            //DataBinding
            //
            SetDishElementDataConextFromListIndex(this.cateOneItem10, cateOneDishes, 0);
            SetDishElementDataConextFromListIndex(this.cateOneItem11, cateOneDishes, 1);
            SetDishElementDataConextFromListIndex(this.cateOneItem20, cateOneDishes, 2);
            SetDishElementDataConextFromListIndex(this.cateOneItem21, cateOneDishes, 3);

            SetDishElementDataConextFromListIndex(this.cateTwoItem10, cateTwoDishes, 0);
            SetDishElementDataConextFromListIndex(this.cateTwoItem11, cateTwoDishes, 1);
            SetDishElementDataConextFromListIndex(this.cateTwoItem20, cateTwoDishes, 2);
            SetDishElementDataConextFromListIndex(this.cateTwoItem21, cateTwoDishes, 3);
        }

        private void BindNewbieTutorial()
        {
            this.NewbieTutorTile.DataContext = newbieTutorial;
        }

        private void SetDishForNewbieTutorial(string dishName)
        {
            string title = string.Format("新手课堂·{0}（视频）", dishName);
            this.dishTutorialForNewbieTitle.Title = title;
        }

        private void SetDishElementDataConextFromListIndex(DishTile dishTile, ObservableCollection<DishTileData> dataList, int index)
        {
            if (index >= 0 && index < dataList.Count)
            {
                dishTile.DataContext = dataList[index];
            }
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
            for (int i = 0; i < 3; i++)
            {
                var mealData = data.DailyMeal.Meals[i];
                dailyMeals[i].Number = mealData.PhotoCount;
                dailyMeals[i].MealImageSource = mealData.ThemeCover;
                dailyMeals[i].Title = mealData.ThemeTitle;
            }

            // Actor
            //
            this.cookMaster.CookMasterImageSource = data.Actor.ActImg;


            // Tutorial
            //
            dishTutorialForNewbieTitle.Title = data.NewbieTutorial.Title;
            newbieTutorial.Teacher.Name = data.NewbieTutorial.UserName;
            newbieTutorial.Teacher.UserPhoto = data.NewbieTutorial.UserAvatar;
            newbieTutorial.MainImageSource = data.NewbieTutorial.RecipeCover;

            newbieTutorial.DetailsImageSources.Clear();
            foreach (var item in data.NewbieTutorial.SamllCovers)
            {
                newbieTutorial.DetailsImageSources.Add(item);
            }
            newbieTutorial.Teacher.ArchiveDescription = data.NewbieTutorial.Intro;


            // Cate One(例如煲一锅幸福爆棚的汤)
            // Cate Two(例如晒一晒)
            //
            this.cateOneTitle.Title = data.CateOne.Title;
            this.cateTwoTitle.Title = data.CateTwo.Title;

            for (int i = 0; i < 4; i++)
            {
                cateOneDishes[i].Author = data.CateOne.Items[i].UserName;
                cateOneDishes[i].DishImageSource = data.CateOne.Items[i].Cover;
                cateOneDishes[i].SupportNumber = data.CateOne.Items[i].Count;

                cateTwoDishes[i].Author = data.CateTwo.Items[i].UserName;
                cateTwoDishes[i].DishImageSource = data.CateTwo.Items[i].Cover;
                cateTwoDishes[i].SupportNumber = data.CateTwo.Items[i].Count;
            }

            // Starred Users
            //
            this.usersData.Clear();
            foreach (var user in data.StarredUser.Users)
            {
                this.usersData.Add(new UserData() { UserPhoto = user.Avatar, Name = user.UserName });
            }

        }


        #endregion

        #region LayoutAdjust

        private void AdjustDishTiles()
        {
            RandomDishTileGroupImageHeight(cateOneItem10, cateOneItem11);
            RandomDishTileGroupImageHeight(cateOneItem20, cateOneItem21);
            RandomDishTileGroupImageHeight(cateTwoItem10, cateTwoItem11);
            RandomDishTileGroupImageHeight(cateTwoItem20, cateTwoItem21);
        }

        private void RandomDishTileGroupImageHeight(DishTile tileOne, DishTile tileTwo)
        {
            double totalHeight = 360;
            double itemOneHeight = random.Next(160, 220);
            double itemTwoHeight = totalHeight - itemOneHeight;

            tileOne.SetImageHight(itemOneHeight);
            tileTwo.SetImageHight(itemTwoHeight);
        }

        #endregion

        #region Test

        private void Test()
        {
        }

        #endregion
    }
}
