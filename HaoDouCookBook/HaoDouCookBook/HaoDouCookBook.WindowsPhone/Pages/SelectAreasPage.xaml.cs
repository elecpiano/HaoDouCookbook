using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectAreasPage : BackablePage
    {
        #region Page Parameter Definition

        public class SelectAreasPagParams
        {
            public Action<string> AfterSelectCompleted { get; set; }

            public SelectAreasPagParams()
            {
                AfterSelectCompleted = null;
            }
        }

        #endregion

        #region Field && Property

        private SelectAreasPageViewModel viewModel = new SelectAreasPageViewModel();
        private SelectAreasPagParams pageParams;

        #endregion

        #region Life Cycle
        
        public SelectAreasPage()
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
            if(e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            pageParams = e.Parameter as SelectAreasPagParams;
            if(pageParams != null)
            {
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                viewModel = new SelectAreasPageViewModel();
                DataBinding();
                LoadAreasDataAsync();
            }

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }


        private async Task LoadAreasDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await InfoAPI.GetAreas(UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.GetInt32UserId(),
                success => { 
                    if(success.Provinces != null)
                    {
                        foreach (var province in success.Provinces)
                        {
                            Province p = new Province();
                            p.ProvinceId = province.ProvinceId;
                            p.ProvinceName = province.ProvinceName;
                            if(province.Cities != null)
                            {
                                foreach (var city in province.Cities)
                                {
                                    p.Cities.Add(new City() { 
                                        CityId = city.CityId,
                                        CityName = city.CityName
                                    });
                                }
                            }

                            viewModel.Provinces.Add(p);
                        }
                    }
                    loading.SetState(LoadingState.DONE);
                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadAreasDataAsync());
                });
        }

        #endregion

        #region Event

        private async void city_Tapped(object sender, TappedRoutedEventArgs e)
        {

            var dataContext = sender.GetDataContext<City>();
            await SettingAPI.UpdateCity(dataContext.CityId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign,
                success => { 
                    if(success.Message.Contains("成功"))
                    {
                        if(pageParams != null && pageParams.AfterSelectCompleted != null)
                        {
                            pageParams.AfterSelectCompleted.Invoke(dataContext.CityName);
                            App.CurrentInstance.RootFrame.GoBack();
                        }
                    }

                    toast.Show(success.Message);
                },
                error => {
                    toast.Show(error.Message);
                });
        }

        #endregion
    }
}
