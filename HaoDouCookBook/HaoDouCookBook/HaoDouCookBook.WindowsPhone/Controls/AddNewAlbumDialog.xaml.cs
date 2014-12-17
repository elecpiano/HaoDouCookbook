using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
