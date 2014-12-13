using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class PersonalTagsPage : BackablePage
    {

        #region Page Parameter Definition

        public class PersonalTagsPageParams
        {
            public Action<string> AfterTagsSetSuccessAction{ get; set; }

            public PersonalTagsPageParams()
            {
                AfterTagsSetSuccessAction = null;
            }
        }

        #endregion

        #region Field && Proprety

        public ObservableCollection<TopicTag> PersonalTags { get; set; }
        private PersonalTagsPageParams pageParams;

        #endregion

        #region Life Cycle

        public PersonalTagsPage()
        {
            this.InitializeComponent();
            PersonalTags = new ObservableCollection<TopicTag>();

            DataBinding();
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

            pageParams = e.Parameter as PersonalTagsPageParams;
            DataBinding();
            LoadTagsDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        private async Task LoadTagsDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await SettingAPI.Fond(UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.GetInt32UserId(),
                success => {
                    if(success.Tags != null)
                    {
                        PersonalTags.Clear();
                        foreach (var item in success.Tags)
                        {
                            PersonalTags.Add(new TopicTag() { 
                                Id = item.Id,
                                Selected = item.Check,
                                Text = item.Name
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);
                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadTagsDataAsync());
                });
        }

        #endregion

        #region Event

        private async void completed_click(object sender, RoutedEventArgs e)
        {
            var selectedTagsId = PersonalTags.Where(t => t.Selected == true).Select(s => s.Id).ToArray();
            string ids = string.Join(",", selectedTagsId);

            await SettingAPI.SetFond(ids, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign,
                success => {
                    if(success.Message.Contains("成功"))
                    {
                        App.Current.RootFrame.GoBack(); 
                        if(pageParams != null && pageParams.AfterTagsSetSuccessAction != null)
                        {
                            var tagsList = PersonalTags.Where(t => t.Selected == true).Select(s => s.Text).ToArray();
                            pageParams.AfterTagsSetSuccessAction.Invoke(string.Join("、", tagsList));
                        }
                    }
                    else
                    {
                        toast.Show(success.Message);
                    }
                },
                error => {
                    if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
                    {
                        toast.Show("网路不稳定，请稍后再试~");
                        return;
                    }

                    toast.Show(error.Message);
                });
        }

        #endregion
    }
}
