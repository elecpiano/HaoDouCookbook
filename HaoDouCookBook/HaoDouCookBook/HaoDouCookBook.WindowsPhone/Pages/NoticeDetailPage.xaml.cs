﻿using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoticeDetailPage : BackablePage
    {
        #region Page Parmeters Definition

        public class NoticeDetailPageParams
        {
            public string SubType { get; set; }

            public NoticeDetailPageParams()
            {
                SubType = string.Empty;
            }
        }

        #endregion

        #region Field && Property

        private NoticePageViewModel viewModel = new NoticePageViewModel();
        private NoticeDetailPageParams pageParams = new NoticeDetailPageParams();

        #endregion

        #region Life Cycle

        public NoticeDetailPage()
        {
            this.InitializeComponent();
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

            pageParams = e.Parameter as NoticeDetailPageParams;
            if (pageParams != null)
            {
                viewModel = new NoticePageViewModel();
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                DataBinding();
                LoadFirstPageDataAsync(pageParams.SubType);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(string subType)
        {
            loading.SetState(LoadingState.LOADING);
            await NoticeAPI.GetUserNotice(0, limit, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid, 0, subType, UserGlobal.Instance.UserInfo.Sign,
                success => {
                    if (success.Notices != null)
                    {
                        viewModel.Notices.Clear();
                        RemoveLoadMoreControl();
                        foreach (var item in success.Notices)
                        {
                            string content = "回复了您，快去看看吧！";
                            int id = 0;
                            if(!string.IsNullOrEmpty(item.Content.Rid))
                            {
                                id = int.Parse(item.Content.Rid);
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(item.Content.Pid))
                                {
                                    id = int.Parse(item.Content.Pid);
                                }
                            }

                            viewModel.Notices.Add(new NoticeItem() { 
                                Avatar = item.Avatar,
                                Content = content,
                                Time = item.Time,
                                Type = item.Type,
                                UserId = item.Uid,
                                UserName = item.UserName,
                                ContentId = id
                            });
                        }

                        if(success.Notices.Length == limit)
                        {
                            EnusureLoadMoreControl();
                        }
                    }

                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);

                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync(subType));
                });
        }


        #endregion

        #region Event

        private void Notice_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<NoticeItem>();
            switch (dataContext.Type)
            {
                case 3:
                    CommentListPage.CommentListPageParams paras3 = new CommentListPage.CommentListPageParams();
                    paras3.RecipeId = dataContext.ContentId;
                    paras3.Type = 3;
                    paras3.Cid = 0;
                    paras3.NoticeType = 3;
                    paras3.SourcePage = CommentListPage.SourcePage.NOTICE_PAGE;

                    App.Current.RootFrame.Navigate(typeof(CommentListPage), paras3);

                    break;
                case 101:
                    CommentListPage.CommentListPageParams paras101 = new CommentListPage.CommentListPageParams();
                    paras101.RecipeId = dataContext.ContentId;
                    paras101.Type = 2;
                    paras101.NoticeType = 101;
                    paras101.Cid = 0;
                    paras101.SourcePage = CommentListPage.SourcePage.NOTICE_PAGE;
                    App.Current.RootFrame.Navigate(typeof(CommentListPage), paras101);
                    break;
                case 103:
                    ProductPage.ProductPageParams pparas = new ProductPage.ProductPageParams();
                    pparas.ProductId = dataContext.ContentId;
                    pparas.TopicId = dataContext.ContentId;
                    pparas.Type = 3;
                    App.Current.RootFrame.Navigate(typeof(ProductPage), pparas);
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region Load More

        int page = 1;
        int limit = 20;

        private NoticeItem loadMoreControlDataContext = new NoticeItem() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (viewModel.Notices != null && !viewModel.Notices.Contains(loadMoreControlDataContext))
            {
                viewModel.Notices.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.Notices != null && viewModel.Notices.Contains(loadMoreControlDataContext))
            {
                viewModel.Notices.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await NoticeAPI.GetUserNotice(
                         page * limit,
                         limit,
                         UserGlobal.Instance.GetInt32UserId(),
                         UserGlobal.Instance.uuid,
                         0,
                         pageParams.SubType,
                         UserGlobal.Instance.UserInfo.Sign,
                         success =>
                         {
                             if (success.Notices != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.Notices)
                                 {
                                     string content = "回复了您，快去看看吧！";
                                     int id = 0;
                                     if (!string.IsNullOrEmpty(item.Content.Rid))
                                     {
                                         id = int.Parse(item.Content.Rid);
                                     }
                                     else
                                     {
                                         if (!string.IsNullOrEmpty(item.Content.Pid))
                                         {
                                             id = int.Parse(item.Content.Pid);
                                         }
                                     }

                                     viewModel.Notices.Add(new NoticeItem()
                                     {
                                         Avatar = item.Avatar,
                                         Content = content,
                                         Time = item.Time,
                                         Type = item.Type,
                                         UserId = item.Uid,
                                         UserName = item.UserName,
                                         ContentId = id
                                     });
                                 }

                                 if (success.Notices.Length == limit)
                                 {
                                     EnusureLoadMoreControl();
                                 }

                                 page++;
                                 loading.SetState(LoadingState.SUCCESS);
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
