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
    public sealed partial class CommentListPage : BackablePage
    {
        #region Page Parameters Definition

        public class CommentListPageParams
        {
            public int Type { get; set; }
            public int Cid { get; set; }
            public int RecipeId { get; set; }

            public CommentListPageParams()
            {
                Type = 1;
                Cid = 0;
                RecipeId = 0;
            }

        }

        #endregion

        #region Field && Property

        private CommentListPageViewModel viewModel = new CommentListPageViewModel();

        #endregion

        #region Life Cycle


        public CommentListPage()
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

            CommentListPageParams paras = e.Parameter as CommentListPageParams;
            if (paras != null)
            {
                viewModel = new CommentListPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadDataAsync(0, 20, paras.Type, paras.Cid, paras.RecipeId);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, int type, int cid, int recipeId)
        {
            await CommentAPI.GetList(offset, limit, type, cid, recipeId, data =>
                {
                    UpdatePageData(data);

                }, error => { });
        }

        private void UpdatePageData(HaoDou.DataModels.Discovery.CommentListPageData data)
        {
            viewModel.UserId = data.Info.UserId;
            viewModel.Image = data.Info.Img;

            if (data.Comments != null)
            {
                foreach (var item in data.Comments)
                {
                    string content = item.Content;
                    if(!string.IsNullOrEmpty(item.AtUserName))
                    {
                        string prefix = string.Format("@{0}:", item.AtUserName);
                        if (content.StartsWith(prefix))
                        {
                            content = content.Substring(prefix.Length);
                        }
                    }

                    viewModel.Comments.Add(new CommentListComment() { 
                            AtUserId = item.AtUserId,
                            AtUserName = item.AtUserName,
                            Avatar = item.Avatar,
                            Cid = item.Cid,
                            Content = content,
                            CreateTime = item.CreateTime,
                            PhotoFlag = item.PhotoFlag,
                            Photold = item.Photold,
                            PhotoUrl = item.PhotoUrl,
                            UserId = item.UserId,
                            UserName = item.UserName
                    });
                }
            }
        }

        #endregion
    }
}
