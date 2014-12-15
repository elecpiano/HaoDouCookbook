using System;
using System.Collections.Generic;
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
    public sealed partial class LoadMoreControl : UserControl
    {
        public Action<LoadMoreControl> LoadMoreAction { get; set; }

        public LoadMoreControl()
        {
            this.InitializeComponent();
        }

        public void SetState(LoadingState state)
        {
            switch (state)
            {
                case LoadingState.LOADING:
                    this.loading.Opacity = 1;
                    this.loading.StartLoading();
                    this.info.Text = "玩命加载中...";
                    break;
                case LoadingState.NETWORK_UNAVAILABLE:
                    this.loading.StopLoading();
                    this.loading.Opacity = 0;
                    this.info.Text = "网路出错啦~~稍后再试试";
                    break;
                case LoadingState.SUCCESS:
                    this.loading.StopLoading();
                    this.loading.Opacity = 0;
                    this.info.Text = "点我加载更多";
                    break;
                case LoadingState.DONE:
                    this.loading.StopLoading();
                    this.loading.Opacity = 0;
                    this.info.Text = "好像没有了哦~再点我试试看~~";
                    break;
                default:
                    break;
            }
        }

        private void LoadMore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(LoadMoreAction != null)
            {
                LoadMoreAction.Invoke(this);
            }
        }

    }
}
