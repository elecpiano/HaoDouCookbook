using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Shared.Utility;
using HaoDouCookBook.Pages;
using System;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class FavoriteTopics : UserControl
    {
        #region Field && Proprety

        public ObservableCollection<FavoriteTopicItem> Topics { get; set; } 

        public Action<string> DeleteSingleSuccessAction { get; set; }

        public Action<Error> DeleteSingleFailAction { get; set; }

        public Action<string> DeleteAllSuccessAction { get; set; }

        public Action<Error> DeleteAllFailAction { get; set; }

        #endregion

        #region Life Cycle

        public FavoriteTopics()
        {
            this.InitializeComponent();
            Topics = new ObservableCollection<FavoriteTopicItem>();
            DataBinding();
            noItemsText.Text = Constants.NO_FAVORITE_TOPICS;
        }

        #endregion

        #region Public Methods

        public async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await FavoriteAPI.GetGroupList(0, 20, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.uuid, data =>
                {
                    Topics.Clear();

                    if(data.Topics != null)
                    {
                        foreach (var item in data.Topics)
                        {
                            Topics.Add(new FavoriteTopicItem() { 
                                Cover = item.Cover,
                                Intro = item.Contents,
                                TopicId = item.Cid,
                                Title = item.Title,
                                Url = item.Url
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);
                },
                error => 
                {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync());
                });
        }

        #endregion

        #region Private Methods

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        #endregion

        #region Event

        private void Topic_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteTopicItem>();
            ArticleViewer.ArticleViewerPageParams paras = new ArticleViewer.ArticleViewerPageParams();
            paras.Url = dataContext.Url;
            paras.TopicId = dataContext.TopicId;

            App.Current.RootFrame.Navigate(typeof(ArticleViewer), paras);
        }

        private void Topic_Holding(object sender, HoldingRoutedEventArgs e)
        {
            sender.ShowFlayout();
        }

        private async void AllDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteTopicItem>();

            await Utilities.ShowOKCancelDialog("提示", "您确定要删除吗？", async () =>
            {
                await FavoriteAPI.Del(UserGlobal.Instance.GetInt32UserId(), 3, dataContext.TopicId.ToString(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, true,
                    async success =>
                    {
                        if (DeleteAllSuccessAction != null)
                        {
                            DeleteAllSuccessAction.Invoke(success.Message);
                        }
                        await LoadFirstPageDataAsync();
                    },
                    error =>
                    {
                        if (DeleteAllFailAction != null)
                        {
                            DeleteAllFailAction.Invoke(error);
                        }
                    });
            }, null);
        }

        private async void Delete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteTopicItem>();

            await Utilities.ShowOKCancelDialog("提示", "您确定要删除吗？", async () =>
            {
                await FavoriteAPI.Del(UserGlobal.Instance.GetInt32UserId(), 3, dataContext.TopicId.ToString(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, false,
                    async success =>
                    {
                        if (DeleteSingleSuccessAction != null)
                        {
                            DeleteSingleSuccessAction.Invoke(success.Message);
                        }
                        await LoadFirstPageDataAsync();
                    },
                    error =>
                    {
                        if (DeleteSingleFailAction != null)
                        {
                            DeleteSingleFailAction.Invoke(error);
                        }
                    });
            }, null);
        }

        #endregion
    }
}
