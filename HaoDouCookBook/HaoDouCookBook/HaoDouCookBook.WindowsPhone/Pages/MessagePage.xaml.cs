using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.HaoDou.DataModels.My;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessagePage : BackablePage
    {
        #region Field && Proprety

        private MessagePageViewModel viewModel = new MessagePageViewModel();

        #endregion

        #region Life Cycle

        public MessagePage()
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
                ReloadFirtPageDataAsync();
                return;
            }

            this.rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            LoadFirstDataAysnc();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstDataAysnc()
        {
            loading.SetState(LoadingState.LOADING);
            await MessageAPI.GetListByUid(0, limit, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    UpdateData(data);
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);
                    
                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstDataAysnc());
                });
        }

        private async Task ReloadFirtPageDataAsync()
        {
            await MessageAPI.GetListByUid(0, limit, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, data =>
            {
                UpdateData(data);
                page = 1;

            }, error => { });
        }

        private void UpdateData(MessagePageData data)
        {
            if (data.Notices != null && data.Notices.Count > 0)
            {
                viewModel.NoticeContent = data.Notices[0].Content;
                viewModel.SubType = data.Notices[0].SubType;
            }

            if (data.Messages != null)
            {
                viewModel.Messages.Clear();
                RemoveLoadMoreControl();
                foreach (var item in data.Messages)
                {
                    viewModel.Messages.Add(new Message()
                    {
                        Avatar = item.Avatar,
                        Content = item.LastMsg,
                        UnreadCount = item.UnreadCount,
                        UpdateTime = item.UpdateTime,
                        UserName = item.UserName,
                        UserId = item.ContactId,
                        MessageId = item.MessageId
                    });
                }

                if(data.Messages.Count == limit)
                {
                    EnusureLoadMoreControl();
                }
            }
        }

        #endregion

        #region Eevnt

        private void Notice_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NoticeDetailPage.NoticeDetailPageParams paras = new NoticeDetailPage.NoticeDetailPageParams();
            App.Current.RootFrame.Navigate(typeof(NoticeDetailPage), paras);
        }
        private void Message_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<Message>();
            IMPage.IMPageParams paras = new IMPage.IMPageParams();
            paras.messageId = dataContext.MessageId.ToString();
            paras.userId = dataContext.UserId;
            paras.UserName = dataContext.UserName;

            App.Current.RootFrame.Navigate(typeof(IMPage), paras);
        }

        #endregion


        #region Load More

        int page = 1;
        int limit = 20;

        private Message loadMoreControlDataContext = new Message() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (viewModel.Messages != null && !viewModel.Messages.Contains(loadMoreControlDataContext))
            {
                viewModel.Messages.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.Messages != null && viewModel.Messages.Contains(loadMoreControlDataContext))
            {
                viewModel.Messages.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await MessageAPI.GetListByUid(
                         page * limit,
                         limit,
                         UserGlobal.Instance.GetInt32UserId(),
                         UserGlobal.Instance.uuid,
                         UserGlobal.Instance.UserInfo.Sign,
                         success =>
                         {
                             if (success.Messages != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.Messages)
                                 {
                                     viewModel.Messages.Add(new Message()
                                     {
                                         Avatar = item.Avatar,
                                         Content = item.LastMsg,
                                         UnreadCount = item.UnreadCount,
                                         UpdateTime = item.UpdateTime,
                                         UserName = item.UserName,
                                         UserId = item.ContactId,
                                         MessageId = item.MessageId
                                     });
                                 }

                                 if (success.Messages.Count == limit)
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
