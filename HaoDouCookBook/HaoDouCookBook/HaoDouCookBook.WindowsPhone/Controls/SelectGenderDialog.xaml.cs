using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Controls
{
    public sealed partial class SelectGenderDialog : ContentDialog
    {
        #region Field && Property

        public Action<string> SetCompletedAction { get; set; }

        public Action<string> SetFailedAction { get; set; }

        #endregion

        #region Life Cycle

        public SelectGenderDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Event

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null && fe.Tag != null)
            {
                string tag = fe.Tag.ToString();
                if (tag.Equals("male", StringComparison.OrdinalIgnoreCase))
                {
                    SetGender(true);
                }
                else
                {
                    SetGender(false);
                }
            }
        }

        #endregion

        #region Private Methods

        private async void SetGender(bool male)
        {
            int gender = male ? 1 : 0;

            await SettingAPI.UpdateGender(gender, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    if(success.Message.Contains("成功"))
                    {
                        if (SetCompletedAction != null)
                        {
                            SetCompletedAction.Invoke( male ? "男" : "女");
                        }
                        this.Hide();
                    }
                    else
                    {
                        toast.Show(success.Message);
                    }
                },
                error =>
                {
                    if (SetFailedAction != null)
                    {
                        SetFailedAction.Invoke(error.Message);
                    }
                });

        }

        #endregion
    }
}
