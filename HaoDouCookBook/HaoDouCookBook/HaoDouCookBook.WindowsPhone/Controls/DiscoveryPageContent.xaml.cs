using HaoDouCookBook.Common;
using HaoDouCookBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private ObservableCollection<DishTileData> Soups = new ObservableCollection<DishTileData>();
        private ObservableCollection<DishTileData> ShaiShaiDishes = new ObservableCollection<DishTileData>();
        private ObservableCollection<UserData> usersData = new ObservableCollection<UserData>();

        private Meal breakfast = new Meal() { Name = "早餐" };
        private Meal luch = new Meal() { Name = "午餐" };
        private Meal dinner = new Meal() { Name = "晚餐" };
        private CookMaster cookMaster = new CookMaster();

        private Random random = new Random();

        #endregion

        #region Life Cycle

        public DiscoveryPageContent()
        {
            this.InitializeComponent();
            this.Loaded += DiscoveryPageContent_Loaded;
        }

        void DiscoveryPageContent_Loaded(object sender, RoutedEventArgs e)
        {
            Test();
            DataBinding();
            AdjustDishTiles();
            SetNewbieTutorial();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            BindDishes();
            BindMeales();
            BindCookMaster();
            BindUserData();
        }

        private void BindUserData()
        {
            this.userMasterList.ItemsSource = usersData;
        }

        private void BindMeales()
        {
            this.mealLeft.DataContext = dinner;
            this.mealRightTop.DataContext = luch;
            this.mealRightBottom.DataContext = breakfast;
        }

        private void BindCookMaster()
        {
            this.cookMasterImage.DataContext = cookMaster;
        }

        private void BindDishes()
        {
            //DataBinding
            //
            SetDishElementDataConextFromListIndex(this.soupItem10, Soups, 0);
            SetDishElementDataConextFromListIndex(this.soupItem11, Soups, 1);
            SetDishElementDataConextFromListIndex(this.soupItem20, Soups, 2);
            SetDishElementDataConextFromListIndex(this.soupItem21, Soups, 3);

            SetDishElementDataConextFromListIndex(this.shaiShaiItem10, ShaiShaiDishes, 0);
            SetDishElementDataConextFromListIndex(this.shaiShaiItem11, ShaiShaiDishes, 1);
            SetDishElementDataConextFromListIndex(this.shaiShaiItem20, ShaiShaiDishes, 2);
            SetDishElementDataConextFromListIndex(this.shaiShaiItem21, ShaiShaiDishes, 3);
        }

        private void SetNewbieTutorial()
        {
            SetDishForNewbieTutorial("板栗烧鸡");
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

        #endregion

        #region LayoutAdjust

        private void AdjustDishTiles()
        {
            RandomDishTileGroupImageHeight(soupItem10, soupItem11);
            RandomDishTileGroupImageHeight(soupItem20, soupItem21);
            RandomDishTileGroupImageHeight(shaiShaiItem10, shaiShaiItem11);
            RandomDishTileGroupImageHeight(shaiShaiItem20, shaiShaiItem21);
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
            // meals
            //
            breakfast.Number = 4352;
            breakfast.MealImageSource = Constants.DEFAULT_TOPIC_IMAGE;
            luch.Number = 12323;
            luch.MealImageSource = Constants.DEFAULT_TOPIC_IMAGE;
            dinner.Number = 343;
            dinner.MealImageSource = Constants.DEFAULT_TOPIC_IMAGE;

            // Cook master
            //
            cookMaster.CookMasterImageSource = Constants.DEFAULT_TOPIC_IMAGE;

            // soups
            //
            Soups.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "93870920", SupportNumber = 24 });
            Soups.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "寡人正在努力中", SupportNumber = 3000000 });
            Soups.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "有情姐姐", SupportNumber = 0 });
            Soups.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "陈政委", SupportNumber = 31 });

            // shai shai 
            //
            ShaiShaiDishes.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "mschimmey", SupportNumber = 24 });
            ShaiShaiDishes.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "爱做美食的小围巾", SupportNumber = 20 });
            ShaiShaiDishes.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "YILIAN", SupportNumber = 2 });
            ShaiShaiDishes.Add(new DishTileData() { DishImageSource = Constants.DEFAULT_TOPIC_IMAGE, Author = "smile", SupportNumber = 2342344 });


            // users
            //
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersData.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
        }

        #endregion

    }
}
