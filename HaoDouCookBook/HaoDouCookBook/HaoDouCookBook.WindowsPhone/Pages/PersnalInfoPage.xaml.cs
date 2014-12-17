using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonalInfoPage : BackablePage, IFileOpenPickerContinuable
    {
        #region Field && Property

        private PersonalInfoPageViewModel viewModel = new PersonalInfoPageViewModel();

        #endregion

        #region Life Cycle

        public PersonalInfoPage()
        {
            this.InitializeComponent();
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

            DataBinding();
            LoadPersonInfoAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadPersonInfoAsync()
        {
            int uid = UserGlobal.Instance.GetInt32UserId();
            await RecipeUserAPI.GetUserInfo(uid, uid, UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    viewModel.Avatar = success.SummaryInfo.Avatar;
                    viewModel.UserName = success.SummaryInfo.UserName;
                    viewModel.Intro =success.SummaryInfo.Intro;
                    viewModel.Gender = success.SummaryInfo.Gender != 0 ? "男" : "女";
                    viewModel.CityName = success.SummaryInfo.Area.CityName;
                    viewModel.IsModity = success.SummaryInfo.IsModify == 1 ? true : false;
                    string tag = "点击修改";
                    if(success.SummaryInfo.Favorite != null)
                    {
                        tag = string.Join("、", success.SummaryInfo.Favorite);
                    }

                    if(string.IsNullOrEmpty(tag))
                    {
                        viewModel.Tags = "点击修改";
                    }
                    else
                    {
                        viewModel.Tags = tag;
                    }

                    UserGlobal.Instance.UpdateUserInfoBySummary(success.SummaryInfo);
                },
                error =>
                {
                    toast.Show(error.Message);
                });
        }

        #endregion

        #region Event

        private void UserName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
            paras.Text = viewModel.UserName;
            paras.Description = Constants.MODIFY_NAME_DESCRIPTION;
            paras.PlaceholderText = "输入新的昵称";
            paras.GoBackAfterConfirmon = false;
            paras.ConfirmAction = async newName =>
            {
                if (viewModel.IsModity)
                {
                    await Utilities.ShowOKCancelDialog(string.Empty, Constants.MOIDFIED_DESCRIPTION, () =>
                        {
                            UpdateUserName(newName);
                        }, null);
                }
                else
                {
                    UpdateUserName(newName);
                }
            };

            App.CurrentInstance.RootFrame.Navigate(typeof(BigTextBox), paras);
        }

        private async void UpdateUserName(string newUserName)
        {
            await SettingAPI.UpdateUserName( 
                newUserName,
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.UserInfo.Sign,
                success => {
                    toast.Show(success.Message);
                },
                error => {
                    toast.Show(error.Message);
                });

            App.CurrentInstance.RootFrame.GoBack();
        }

        private void intro_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
            paras.PlaceholderText = "请输入新的个性化签名";
            paras.Text = viewModel.Intro;
            paras.TextWrapping = TextWrapping.Wrap;
            paras.AcceptReturn = true;
            paras.ConfirmAction = async newIntro =>
            {
                await AccountAPI.ChangeIntro(newIntro, UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.GetInt32UserId(),
                    success => {
                        if(success.Message.Contains("修改成功"))
                        {
                            viewModel.Intro = newIntro;
                        }
                        toast.Show(success.Message);
                    },
                    error => {
                        toast.Show(error.Message);
                    });
            };

            App.CurrentInstance.RootFrame.Navigate(typeof(BigTextBox), paras);

        }

        private void tags_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PersonalTagsPage.PersonalTagsPageParams paras = new PersonalTagsPage.PersonalTagsPageParams();
            paras.AfterTagsSetSuccessAction = tags => { 
                if(string.IsNullOrEmpty(tags))
                {
                    viewModel.Tags = "点击修改";
                    return;
                }

                viewModel.Tags = tags; 
            };

            App.CurrentInstance.RootFrame.Navigate(typeof(PersonalTagsPage), paras);
        }

        private async void selectPhoto_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectPhotoDialog dialog = new SelectPhotoDialog();
            await dialog.ShowAsync();
        }

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if(args.Files != null && args.Files.Count > 0)
            {
                Shared.Utility.ImageHelper helper = new Shared.Utility.ImageHelper();
            }
        }

        private async void Gender_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectGenderDialog dialog = new SelectGenderDialog();
            dialog.SetCompletedAction = gender =>
                {
                    viewModel.Gender = gender;
                    toast.Show("修改成功");
                };
            dialog.SetFailedAction = error => toast.Show(error);
            await dialog.ShowAsync();
        }

        #endregion

        private void City_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectAreasPage.SelectAreasPagParams paras = new SelectAreasPage.SelectAreasPagParams();
            paras.AfterSelectCompleted = newCity =>
            {
                viewModel.CityName = newCity;
                toast.Show("修改成功");
            };

            App.CurrentInstance.RootFrame.Navigate(typeof(SelectAreasPage), paras);
        }
    }
}
