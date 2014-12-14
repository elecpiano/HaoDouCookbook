using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Controls
{
    public sealed partial class AddNewAlbumDialog : ContentDialog
    {
        public Action<string> ActionAfterAlbumCreated { get; set; }

        public AddNewAlbumDialog()
        {
            this.InitializeComponent();
        }

        private void Canncel_click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private async void OK_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.textbox.Text.Trim()))
            {
                toast.Show("分类名字不能为空");
                return;
            }

            await FavoriteAPI.AddMyAlbum(
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.UserInfo.Sign,
                this.textbox.Text.Trim(),
                success => {
                    if (ActionAfterAlbumCreated != null)
                    {
                        ActionAfterAlbumCreated.Invoke(success.Message);
                        this.Hide();
                    }
                },
                error => {
                    toast.Show(error.Message);
                });

        }

    }
}
