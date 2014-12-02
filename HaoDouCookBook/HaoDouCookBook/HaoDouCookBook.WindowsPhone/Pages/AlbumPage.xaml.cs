using HaoDouCookBook.ViewModels;
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
using Shared.Utility;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumPage : BackablePage
    {
        #region Page Parameters Definition 

        public class AlbumPageParams
        {
            public int AlbumId { get; set; }

            public AlbumPageParams()
            {
                AlbumId = -1;
            }
 
        }

        #endregion

        #region Field && Property

        private AlbumPageViewModel viewModel = new AlbumPageViewModel();

        #endregion

        #region Life Cycle

        public AlbumPage()
        {
            this.InitializeComponent();
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
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            AlbumPageParams paras = e.Parameter as AlbumPageParams;
            if (paras != null)
            {
                viewModel = new AlbumPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                LoadDataAsync(0, 20, paras.AlbumId, string.Empty, null);
            }
        }
       

        #endregion

        #region Data Prepare
        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, int albumId, string sign, int? uid)
        {
            await InfoAPI.GetAlbumInfo(offset, limit, albumId, sign, uid, DeviceHelper.GetUniqueDeviceID(), data =>
                {
                    DataBinding();
                    viewModel.AlbumAvatar = data.Info.AlbumAvatarUrl;
                    viewModel.AlbumContent = data.Info.AlbumContent;
                    viewModel.AlbumCover = data.Info.AlbumCover;
                    viewModel.AlbumIsLike = data.Info.AlbumIsLike == 0 ? false : true;
                    viewModel.AlbumTitle = data.Info.AlbumTitle;
                    viewModel.AlbumUserId = data.Info.AlbumUserId;
                    viewModel.AlbumUserName = data.Info.AlbumUserName;
                    if(!string.IsNullOrEmpty(data.Info.CommentCount))
                    {
                        viewModel.CommentCount = int.Parse(data.Info.CommentCount);
                    }

                    if (data.Recipes != null)
                    {
                        foreach (var item in data.Recipes)
                        {
                            viewModel.Recipes.Add(new RecipeTileData() { 
                                RecipeId = item.RecipeId,
                                SupportNumber = item.LikeCount.ToString(),
                                Recommendation = item.Intro,
                                Author = item.UserName,
                                RecipeImage = item.Cover,
                                RecipeName = item.Title
                            });
                        }
                    }
 
                }, error => { });
        }

        #endregion

        #region Event

        private void RecipeTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipeTileData>(); 
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = dataContext.RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }
        
        #endregion
    }
}
