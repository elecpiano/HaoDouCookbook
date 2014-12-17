using HaoDouCookBook.Controls;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeebackHistory : BackablePage
    {
        public ObservableCollection<ViewModels.Feedback> Feedbacks { get; set; }
        
        public FeebackHistory()
        {
            this.InitializeComponent();
            Feedbacks = new ObservableCollection<ViewModels.Feedback>();
            this.noItemsText.Text = Constants.NO_FEEDBACK;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            DataBinding();
            LoadFirstPageDataAsync();
        }

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await FeedbackAPI.GetList(
                0,
                limit,
                UserGlobal.Instance.uuid,
                success => {
                    if(success.Feedbacks != null)
                    {
                        RemoveLoadMoreControl();
                        Feedbacks.Clear();
                        foreach (var item in success.Feedbacks)
                        {
                            Feedbacks.Add(new ViewModels.Feedback() { 
                                Content = item.Content,
                                CreateTime = item.CreateTime
                            });
                        }

                        if(success.Feedbacks.Length == limit)
                        {
                            EnsureLoadMoreControl();
                        }
                    }
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);
                },
                error => {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataAsync());
                });
        }

        private async void cleaAll_Tapped(object sender, RoutedEventArgs e)
        {
            await Utilities.ShowOKCancelDialog("提示", "您确定要清空吗？", async () =>
            {
                await FeedbackAPI.Clear(
                    UserGlobal.Instance.uuid,
                    success =>
                    {
                        if (success.Message.Contains("清空成功"))
                        {
                            Feedbacks.Clear();
                        }

                        toast.Show(success.Message);
                    },
                    error =>
                    {
                        toast.Show(error.Message);
                    });

            }, null);
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 2;

        private ViewModels.Feedback loadMoreControlDataContext = new ViewModels.Feedback() { IsLoadMore = true };

        public void EnsureLoadMoreControl()
        {
            if (Feedbacks != null && !Feedbacks.Contains(loadMoreControlDataContext))
            {
                Feedbacks.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (Feedbacks != null && Feedbacks.Contains(loadMoreControlDataContext))
            {
                Feedbacks.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await FeedbackAPI.GetList(
                         page * limit,
                         limit,
                         UserGlobal.Instance.uuid,
                         success =>
                         {
                             if (success.Feedbacks != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.Feedbacks)
                                 {
                                     Feedbacks.Add(new ViewModels.Feedback() {
                                         Content = item.Content,
                                         CreateTime = item.CreateTime,
                                     });
                                 }

                                 if(success.Feedbacks.Length == limit)
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
