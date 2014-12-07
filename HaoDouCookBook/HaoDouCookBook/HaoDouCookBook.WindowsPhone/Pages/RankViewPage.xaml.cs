using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RankViewPage : BackablePage
    {
        #region Page Parameters Definition

        public class RankViewPageParams
        {
            public int Id { get; set; }
            public int Type { get; set; }

            public RankViewPageParams()
            {

            }
        }

        #endregion

        #region Field && Property

        private RankViewPageViewModel viewModel = new RankViewPageViewModel();

        #endregion

        #region Life Cycle

        public RankViewPage()
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

            RankViewPageParams paras = e.Parameter as RankViewPageParams;
            if(paras != null)
            {
                viewModel = new RankViewPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                LoadDataAsync(paras.Id, paras.Type, string.Empty, null);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int id, int type, string sign, int? uid)
        {
            await RankAPI.GetRankView(id, sign, uid, UserGlobal.Instance.uuid, type, data =>
                {
                    DataBinding();

                    viewModel.Title = data.Info.Title;
                    viewModel.Intro = data.Info.Intro;

                    if (data.Recipes != null)
                    {
                        for (int i = 0; i < data.Recipes.Length; i++)
                        {
                            var item = data.Recipes[i];

                            string rankIcon = string.Empty;

                            switch (i)
                            {
                                case 0:
                                    rankIcon = "../assets/images/rank/rank-1.png";
                                    break;
                                case 1:
                                    rankIcon = "../assets/images/rank/rank-2.png";
                                    break;
                                case 2:
                                    rankIcon = "../assets/images/rank/rank-3.png";
                                    break;
                                default:
                                    rankIcon = "../assets/images/rank/rank-4.png";
                                    break;
                            }

                            viewModel.Recipes.Add(new RankViewRecipeItem()
                            {
                                Title = item.Title,
                                Cover = item.Cover,
                                UserName =item.UserName,
                                FavoriteCount = item.FavoriteCount,
                                IsFavorited = item.IsFavorited == 1 ? true : false,
                                RecipeId = item.RecipeId,
                                RankIcon = rankIcon,
                                Rank = i + 1
                            });
                        }
                    }


                }, error => { });
        }

        #endregion

        #region Event

        private void ShowAllRanks_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RankListPage));
        }
        private void RecipeItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = sender.GetDataContext<RankViewRecipeItem>().RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        #endregion

       
    }
}
