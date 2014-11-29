using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
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
    public sealed partial class StuffInfoPage : BackablePage
    {
        #region Page Parameters Definition

        public class StuffInfoPageParams
        {
            public string Title { get; set; }

            public int Id { get; set; }

            public StuffInfoPageParams()
            {

            }
        }

        #endregion

        #region Field && Property

        private StuffInfoViewModel viewModel = new StuffInfoViewModel();

        #endregion

        #region Life Cycle

        public StuffInfoPage()
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

            StuffInfoPageParams paras = e.Parameter as StuffInfoPageParams;
            if (paras != null)
            {
                viewModel = new StuffInfoViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                viewModel.StuffName = paras.Title;
                DataBinding();
                LoadDataAsync(paras.Id);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int id)
        {
            await StuffAPI.GetStuffInfo(id, data =>
                {
                    viewModel.Cover = data.Info.Cover;
                    viewModel.Effect = data.Info.Effect;
                    viewModel.Intro = data.Info.Intro;
                    viewModel.Pick = data.Info.Pick;
                    viewModel.Skill = data.Info.Skill;
                    viewModel.Storage = data.Info.Storage;

                    if (data.Info.RecipeList != null)
                    {
                        foreach (var item in data.Info.RecipeList)
                        {
                            viewModel.Recipes.Add(new StuffRecipe() { 
                                    Cover = item.Cover,
                                    RecipeId = item.RecipeId,
                                    Title = item.Title
                            });
                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event


        private void ShowAllRecipes_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TagsPage.TagPageParams paras = new TagsPage.TagPageParams();
            paras.Id = null;
            paras.TagText = viewModel.StuffName;

            App.Current.RootFrame.Navigate(typeof(TagsPage), paras);
        }

        #endregion
    }
}
