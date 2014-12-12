using Shared.Animations;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Shared.Utility;
using HaoDouCookBook.ViewModels;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Common;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class ActivitySupportAndComment : UserControl
    {
        private bool isExpand = false;

        public Toast Toast { get; set; }

        public ActivitySupportAndComment()
        {
            this.InitializeComponent();
            ScaleAnimation.ScaleTo(this.contentPanel, 0, 1, TimeSpan.FromSeconds(0), fe => { isExpand = false; });
            this.Loaded += ActivitySupportAndComment_Loaded;
        }

        void ActivitySupportAndComment_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<UserActivityItem>();
            if (dataContext != null)
            {
                if (dataContext.IsDigg)
                {
                    this.support.Text = "取消";
                }
                else
                {
                    this.support.Text = "赞";
                }
            }
        }

        private void ShowPannel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (isExpand)
            {
                Collapsed();
            }
            else
            {
                Expand();
            }
        }

        private void Expand()
        {
            ScaleAnimation.ScaleTo(this.contentPanel, 1, 1, TimeSpan.FromSeconds(0.3), fe =>
            {
                isExpand = true;
            }); 
        }

        private void Collapsed()
        {
            ScaleAnimation.ScaleTo(this.contentPanel, 0, 1, TimeSpan.FromSeconds(0.3), fe =>
            {
                isExpand = false;
            });
        }

        private async void Support_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<UserActivityItem>();
            if (dataContext != null)
            {
                if(dataContext.IsDigg)
                {
                    await DiggAPI.Digg(dataContext.ActivityId, UserGlobal.Instance.GetInt32UserId(), 2, 0, UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign,
                        success => {
                            if(success.Message == "操作成功")
                            {
                                dataContext.IsDigg = false;
                                dataContext.DiggCount--;
                                this.support.Text = "赞";
                            }

                            if (Toast != null)
                            {
                                Toast.Show(success.Message);
                            }
                        },
                        error => {
                            if (Toast != null)
                            {
                                Toast.Show(error.Message);
                            }
                        });
                    
                }
                else
                {
                    await DiggAPI.Digg(dataContext.ActivityId, UserGlobal.Instance.GetInt32UserId(), 2, 1, UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign,
                        success => {
                            if(success.Message == "操作成功")
                            {
                                dataContext.IsDigg = true;
                                dataContext.DiggCount++;
                                this.support.Text = "取消";
                            }

                            if (Toast != null)
                            {
                                Toast.Show(success.Message);
                            }
                        },
                        error => {
                            if (Toast != null)
                            {
                                Toast.Show(error.Message);
                            }
                        });
                }
            }

            Collapsed();
        }

        private void Comment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Collapsed();
        }
    }
}
