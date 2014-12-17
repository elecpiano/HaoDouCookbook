using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdRecommendationPage : BackablePage
    {
        #region Field && Property

        private AdRecommendationPageViewModel viewModel = new AdRecommendationPageViewModel();

        #endregion

        #region Life Cycle

        public AdRecommendationPage()
        {
            this.InitializeComponent();
            DataBinding();
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return; 
            }

            viewModel = new AdRecommendationPageViewModel();
            rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            DataBinding();
            await LoadDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await AdAPI.GetRecommendAdList(0, UserGlobal.Instance.uuid, data =>
                {
                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            viewModel.AdItems.Add(new AdRecommendationItem() { 
                                    Description = item.Desc,
                                    Image = item.Image,
                                    Title = item.Title,
                                    Url = item.Url
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);

                }, error =>
                {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadDataAsync());
                });
        }

        #endregion

        #region Event

        private async void GoDownload_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<AdRecommendationItem>();
            if (dataContext != null)
            {
                await DeviceHelper.OpenHttpURL(dataContext.Url);
            }
        }

        #endregion
    }
}
