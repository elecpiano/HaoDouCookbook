using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Shared.Utility;
using HaoDouCookBook.Pages;
using HaoDouCookBook.HaoDou.DataModels.My;
using System.Linq;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserActivity : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty NoItemsTextProperty = DependencyProperty.Register("NoItemsText", typeof(string), typeof(UserActivity), new PropertyMetadata(string.Empty));

        #endregion

        #region Field && CLR Proprety Wrapper

        public string NoItemsText
        {
            get { return (string)GetValue(NoItemsTextProperty); }
            set { SetValue(NoItemsTextProperty, value); }
        }

        private ObservableCollection<UserActivitiesGroup> ActivitiesGroups = new ObservableCollection<UserActivitiesGroup>();
        private int userId;

        #endregion

        #region Life Cycle

        public UserActivity()
        {
            this.InitializeComponent();
        }

        #endregion

        #region DataBinding

        private void DataBinding()
        {
            this.list.ItemsSource = ActivitiesGroups;
            this.noItemsPanel.DataContext = ActivitiesGroups;
            this.noItemsText.DataContext = this;
        }

        #endregion

        #region Public Method

        public void ResetScrollViewerToBegin()
        {
            this.rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
        }

        public async Task LoadFirstDataAsync(int userId)
        {
            ResetScrollViewerToBegin();
            this.userId = userId;
            this.loading.SetState(LoadingState.LOADING);
            DataBinding();
            await UserFeedAPI.GetFollowUserFeed(0, limit, userId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    UpdateData(success);
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);

                }, error =>
                {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstDataAsync(userId));
                });
        }

        private void UpdateData(UserActivitiesData data)
        {
            if (data.Activities != null)
            {
                ActivitiesGroups.Clear();
                Dictionary<string, List<UserActivityItem>> dict = new Dictionary<string, List<UserActivityItem>>();
                foreach (var item in data.Activities)
                {

                    UserActivityItem usi = new UserActivityItem()
                    {
                        Name = item.Name,
                        Content = item.Content,
                        CreateTime = item.CreateTime,
                        Image = item.Pic,
                        ProductId = item.ItemId,
                        Type = item.Type,
                    };

                    if (dict.ContainsKey(item.FormatTime))
                    {
                        dict[item.FormatTime].Add(usi);
                    }
                    else
                    {
                        List<UserActivityItem> list = new List<UserActivityItem>();
                        list.Add(usi);
                        dict.Add(item.FormatTime, list);
                    }
                }

                RemoveLoadMoreControl();
                foreach (var kv in dict)
                {
                    UserActivitiesGroup group = new UserActivitiesGroup();
                    string[] temp = kv.Key.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                    group.Month = temp[0].Trim();
                    group.Day = temp[1].Trim();

                    foreach (var item in kv.Value)
                    {
                        group.Activities.Add(item);
                    }
                    ActivitiesGroups.Add(group);
                }

                if (data.Activities.Length == limit)
                {
                    EnusureLoadMoreControl();
                }
            }
        }

        #endregion

        #region Event

        private void activity_Line_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtEnd<UserActivityItem>(sender, false);
        }

        private void activitiesGroup_Line_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtEnd<UserActivitiesGroup>(sender, false);
        }

        private void Activity_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<UserActivityItem>();
            switch (dataContext.Type)
            {
                case 10:
                    RecipeInfoPage.RecipeInfoPageParams para = new RecipeInfoPage.RecipeInfoPageParams();
                    para.RecipeId = dataContext.ProductId;

                    App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), para);
                    break;
                case 30:
                    SingleProductViewPage.SingleProductViewPageParams p = new SingleProductViewPage.SingleProductViewPageParams();
                    p.ProductId = dataContext.ProductId;

                    App.Current.RootFrame.Navigate(typeof(SingleProductViewPage), p);
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Load More

        int page = 1;
        int limit = 20;

        private UserActivitiesGroup loadMoreControlDataContext = new UserActivitiesGroup() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (ActivitiesGroups != null && !ActivitiesGroups.Contains(loadMoreControlDataContext))
            {
                ActivitiesGroups.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (ActivitiesGroups != null && ActivitiesGroups.Contains(loadMoreControlDataContext))
            {
                ActivitiesGroups.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await UserFeedAPI.GetFollowUserFeed(
                         page * limit,
                         limit,
                         this.userId,
                         UserGlobal.Instance.GetInt32UserId(),
                         UserGlobal.Instance.UserInfo.Sign,
                         success =>
                         {
                             if (success.Activities != null)
                             {
                                 #region 预处理数据

                                 Dictionary<string, List<UserActivityItem>> dict = new Dictionary<string, List<UserActivityItem>>();
                                 foreach (var item in success.Activities)
                                 {

                                     UserActivityItem usi = new UserActivityItem()
                                     {
                                         Name = item.Name,
                                         Content = item.Content,
                                         CreateTime = item.CreateTime,
                                         Image = item.Pic,
                                         ProductId = item.ItemId,
                                         Type = item.Type,
                                     };

                                     if (dict.ContainsKey(item.FormatTime))
                                     {
                                         dict[item.FormatTime].Add(usi);
                                     }
                                     else
                                     {
                                         List<UserActivityItem> list = new List<UserActivityItem>();
                                         list.Add(usi);
                                         dict.Add(item.FormatTime, list);
                                     }
                                 }

                                 #endregion

                                 RemoveLoadMoreControl();

                                 var newGroupList = new List<UserActivitiesGroup>();
                                 foreach (var kv in dict)
                                 {
                                     UserActivitiesGroup group = new UserActivitiesGroup();
                                     string[] temp = kv.Key.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                                     group.Month = temp[0].Trim();
                                     group.Day = temp[1].Trim();

                                     foreach (var item in kv.Value)
                                     {
                                         group.Activities.Add(item);
                                     }
                                     newGroupList.Add(group);
                                 }

                                 foreach (var group in newGroupList)
                                 {
                                     //如果存在则加入到已存在组中
                                     //
                                     if (ActivitiesGroups.Any(g => g.Key == group.Key))
                                     {
                                         var groupExist = ActivitiesGroups.FirstOrDefault(g => g.Key == group.Key);
                                         foreach (var item in group.Activities)
                                         {
                                             groupExist.Activities.Add(item);
                                         }
                                     }
                                     else // 如果不存在直接加进去
                                     {
                                         ActivitiesGroups.Add(group);
                                     }
                                 }

                                 if (success.Activities.Length == limit)
                                 {
                                     EnusureLoadMoreControl();
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
