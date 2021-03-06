﻿using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeCategoryDetailPage : BackablePage
    {
        #region Page Parameters Definition

        public class RecipeCategoryDefailPageParams
        {
            public int Id { get; set; }
            public string Title { get; set; }

            public RecipeCategoryDefailPageParams()
            {

            }
        }

        #endregion

        #region Field && Property
        private ObservableCollection<RecipeTileData> Recipes = new ObservableCollection<RecipeTileData>();
        RecipeCategoryDefailPageParams pageParams;
        #endregion

        #region Life Cycle

        public RecipeCategoryDetailPage()
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

            pageParams = e.Parameter as RecipeCategoryDefailPageParams;
            if (pageParams != null)
            {
                // show the bottom appbar if the title is 私人定制
                //
                if (pageParams.Id == 391926 || pageParams.Title == "私人定制")
                {
                    this.bottomAppbar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    this.bottomAppbar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                this.title.Text = pageParams.Title;
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                Recipes.Clear();
                LoadFirstPageDataAsync(pageParams.Title, pageParams.Id);
            }

        }


        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.recipesList.ItemsSource = Recipes;
        }

        private async Task LoadFirstPageDataAsync(string typeName, int recipeId)
        {
            loading.SetState(LoadingState.LOADING);
            await RecipeAPI.GetCollectRecomment(
                0, 
                limit, 
                UserGlobal.Instance.UserInfo.Sign, 
                UserGlobal.Instance.GetInt32UserId(), 
                UserGlobal.Instance.uuid, 
                typeName, 
                recipeId, 
                success =>
                {
                    if(success.Recipes != null)
                    {
                        RemoveLoadMoreControl();
                        foreach (var item in success.Recipes)
                        {
                            Recipes.Add(new RecipeTileData()
                            {
                                Author = item.UserName,
                                TagsText = item.GetTagsString(),
                                RecipeImage = item.Cover,
                                RecipeName = item.Title,
                                SupportNumber = item.LikeCount.ToString(),
                                RecipeId = item.RecipeId
                            });

                            page = 1;
                        }

                        if(success.Recipes.Length == limit)
                        {
                            EnsureLoadMoreControl();
                        }
                    }
                    loading.SetState(LoadingState.SUCCESS);

                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async ()=> await LoadFirstPageDataAsync(typeName, recipeId));
                });
        }

        #endregion

        #region Event

        private void RecipeTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipeTileData>(); 
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = dataContext.RecipeId;
            App.CurrentInstance.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        private void personalTags_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(Utilities.SignedIn())
            {
                App.CurrentInstance.RootFrame.Navigate(typeof(PersonalTagsPage));
            }
            else
            {
                LoginPage.LoginPageParams paras = new LoginPage.LoginPageParams();
                paras.SignedInAction = () =>  toast.Show("登录成功");

                App.CurrentInstance.RootFrame.Navigate(typeof(LoginPage), paras);
            }
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 10;

        private RecipeTileData loadMoreControlDataContext = new RecipeTileData() { IsLoadMore = true };

        public void EnsureLoadMoreControl()
        {
           if(Recipes != null && !Recipes.Contains(loadMoreControlDataContext))
           {
               Recipes.Add(loadMoreControlDataContext);
           }
        }

        public void RemoveLoadMoreControl()
        {
            if (Recipes != null && Recipes.Contains(loadMoreControlDataContext))
            {
                Recipes.Remove(loadMoreControlDataContext);
            } 
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await RecipeAPI.GetCollectRecomment(
                         page * limit,
                         limit,
                         UserGlobal.Instance.UserInfo.Sign,
                         UserGlobal.Instance.GetInt32UserId(),
                         UserGlobal.Instance.uuid,
                         pageParams.Title,
                         pageParams.Id,
                         success =>
                         {
                             if (success.Recipes != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.Recipes)
                                 {
                                     Recipes.Add(new RecipeTileData()
                                     {
                                         Author = item.UserName,
                                         TagsText = item.GetTagsString(),
                                         RecipeImage = item.Cover,
                                         RecipeName = item.Title,
                                         SupportNumber = item.LikeCount.ToString(),
                                         RecipeId = item.RecipeId
                                     });
                                 }

                                 if(success.Recipes.Length == limit)
                                 {
                                     EnsureLoadMoreControl();
                                 }
                                 page++;
                                 loadmore.SetState(LoadingState.SUCCESS);
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
                });
        }

        #endregion
    }
}
