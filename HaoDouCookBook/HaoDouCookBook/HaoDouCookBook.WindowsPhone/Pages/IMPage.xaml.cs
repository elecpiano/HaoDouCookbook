using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IMPage : BackablePage
    {
        #region Page Parameter Definition

        public class IMPageParams
        {
            public int userId { get; set; }

            public string UserName { get; set; }

            public string messageId { get; set; }

            public IMPageParams()
            {

            }
        }

        #endregion

        #region Filed && Property

        private IMPageViewModel viewModel = new IMPageViewModel();
        private IMPageParams pageParams;

        #endregion

        #region Life Cycle
        public IMPage()
        {
            this.InitializeComponent();
            this.input.SendAction = SendMessageAction;
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

            pageParams = e.Parameter as IMPageParams;
            if (pageParams != null)
            {
                DataBinding();
                viewModel.UserName = pageParams.UserName;
                LoadIMMessageAsync(pageParams.userId, pageParams.messageId);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadIMMessageAsync(int userId, string messageId)
        {
            loading.SetState(LoadingState.LOADING);
            await MessageAPI.GetMessageList(
                UserGlobal.Instance.GetInt32UserId(),
                userId,
                UserGlobal.Instance.UserInfo.Sign,
                messageId,
                UserGlobal.Instance.uuid,
                0,
                success => {
                    if(success.Messages != null)
                    {
                        viewModel.Messages.Clear();
                        foreach (var item in success.Messages)
                        {
                               int _userid = 0;
                            int.TryParse(item.UserId.ToString(), out _userid);

                            viewModel.Messages.Add(new IMMessage() { 
                                CreateTime = item.CreateTime,
                                Message = item.Msg,
                                UserId = _userid,
                                IsSignedInUser = Utilities.IsSignedInUser(_userid),
                                Avatar = item.Avatar
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);
                    rootScrollViewer.ScrollToVerticalOffset(messageList.ActualHeight);
                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadIMMessageAsync(userId, messageId));
                });
        }

        #endregion

        #region Event

        private async void SendMessageAction(string message)
        {
            await MessageAPI.SendMessage(
                message,
                pageParams.userId,
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.UserInfo.Sign,
                UserGlobal.Instance.uuid,
                success => {
                    if (success.Message.Contains("成功"))
                    {
                        viewModel.Messages.Add(new IMMessage() {
                            Avatar = UserGlobal.Instance.UserInfo.Avatar,
                            CreateTime = "刚刚",
                            IsSignedInUser = true,
                            Message = message,
                            UserId = UserGlobal.Instance.GetInt32UserId()
                        });
                    }

                    toast.Show(success.Message);
                    this.input.ClearText();
                },
                error => {
                    toast.Show(error.Message);
                });
        }
        private async void ClearMessage_Click(object sender, RoutedEventArgs e)
        {
            await Utilities.ShowOKCancelDialog("提示", "您确定要删除吗？", async () =>
            {
                await MessageAPI.ClearOneMessageList(pageParams.userId, UserGlobal.Instance.GetInt32UserId(), pageParams.messageId, UserGlobal.Instance.UserInfo.Sign,
                    success =>
                    {
                        if (success.Message.Contains("成功"))
                        {
                            App.Current.RootFrame.GoBack();
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

      
    }
}
