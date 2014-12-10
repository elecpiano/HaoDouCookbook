using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Shared.Utility;
using HaoDouCookBook.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class FavoriteTopics : UserControl
    {
        #region Field && Proprety

        public ObservableCollection<FavoriteTopicItem> Topics { get; set; } 

        #endregion

        #region Life Cycle

        public FavoriteTopics()
        {
            this.InitializeComponent();
            Topics = new ObservableCollection<FavoriteTopicItem>();
            DataBinding();
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
                    if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                    {
                        loading.RetryAction = async () => await LoadFirstPageDataAsync();
                        loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                    }
 
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

            App.Current.RootFrame.Navigate(typeof(ArticleViewer), paras);
        }

        #endregion
    }
}
