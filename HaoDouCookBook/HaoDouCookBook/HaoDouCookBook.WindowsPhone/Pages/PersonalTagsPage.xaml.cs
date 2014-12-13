using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
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
        public ObservableCollection<TopicTag> PersonalTags { get; set; }

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

            DataBinding();
            LoadTagsDataAsync();
        }

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        private async Task LoadTagsDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await SettingAPI.Fond(UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.GetInt32UserId(),
                success => {

                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadTagsDataAsync());
                });
        }
    }
}
