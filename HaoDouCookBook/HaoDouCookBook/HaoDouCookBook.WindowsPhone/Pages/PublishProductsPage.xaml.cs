using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class PublishProductsPage : BackablePage, IFileOpenPickerContinuable
    {
        private PublishProductsPageViewModel viewModel = new PublishProductsPageViewModel();

        public PublishProductsPage()
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

            rootScrollViewer.ScrollToVerticalOffset(0);
            DataBinding();
            LoadTags();
        }

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadTags()
        {
            await RecipePhotoAPI.GetTopicList(success =>
                {
                    if (success.Tags != null)
                    {
                        viewModel.Tags.Clear();
                        foreach (var item in success.Tags)
                        {
                            int id = 0;
                            int.TryParse(item.Id.ToString(), out id);

                            viewModel.Tags.Add(new TopicTag() { 
                                Id = id,
                                Selected = item.Select == 1 ? true : false,
                                Text = item.Title
                            });
                        }
                    }

                }, error => { });
        }

        #endregion

        private async void Cover_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectPhotoDialog dialog = new SelectPhotoDialog();
            await dialog.ShowAsync();
        }

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files != null && args.Files.Count > 0)
            {
                var file = args.Files[0];
                viewModel.Cover = await IsolatedStorageHelper.CopyFromFileAsync(file, Constants.PUBLISH_RECIPES_TEMP_FOLDER);
            }
        }

        private async void publish_click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(viewModel.Cover))
            {
                toast.Show("照片不能为空哦~");
                return;
            }

            var tag = viewModel.Tags.FirstOrDefault(t => t.Selected == true);
            int tagId = 0;
            string tagName = string.Empty;
            if(tag != null)
            {
                tagId = tag.Id;
                tagName = tag.Text;
            }

            StorageFile file = null;

            try
            {
                file = await StorageFile.GetFileFromPathAsync(viewModel.Cover);
            }
            catch(FileNotFoundException)
            {
                return;
            }

            var property = await file.GetBasicPropertiesAsync();
            ulong size = property.Size;

            string content = string.Empty;

            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    content = sr.ReadToEnd();
                }
            }

            await RecipePhotoAPI.Upload(
                string.Empty,
                UserGlobal.Instance.GetInt32UserId(),
                tagId,
                tagName,
                viewModel.Intro,
                viewModel.Name,
                UserGlobal.Instance.UserInfo.Sign,
                UserGlobal.Instance.uuid,
                string.Empty,
                file.Name,
                size,
                content,
                success => { 

                    if(success.Pid > 0)
                    {
                    
                    }

                    toast.Show(success.Message);
                },
                error => {
                    toast.Show(error.Message);
                });
        }
    }
}
