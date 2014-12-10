using HaoDouCookBook.Controls;
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
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoriteRecipeAlbumPage : BackablePage
    {
        #region Page Parameters Definition

        public class FavoriteRecipeAlbumPageParams
        {
            public int AlbumId { get; set; }

            public string Title { get; set; }

            public FavoriteRecipeAlbumPageParams()
            {
                AlbumId = -1;
                Title = string.Empty;
            }
        }

        #endregion

        #region Field && Proprety

        private FavoriteRecipesAlbumPageViewModel viewModel = new FavoriteRecipesAlbumPageViewModel();
        private FavoriteRecipeAlbumPageParams pageParams;

        #endregion

        #region Life Cycle

        public FavoriteRecipeAlbumPage()
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

            pageParams = e.Parameter as FavoriteRecipeAlbumPageParams;
            if (pageParams != null)
            {
                this.rootScrollViewer.ScrollToVerticalOffset(0);
                viewModel = new FavoriteRecipesAlbumPageViewModel();
                DataBinding();
            }
        }

        #endregion

        #region Data Prepare
        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFisrtPageDataAsync(int albumId)
        {
            await InfoAPI.GetAlbumInfo(0, 20, albumId, UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid,
                                        data =>
                                        {

                                        }, error => { 

                                        });
        }

        #endregion
    }
}
