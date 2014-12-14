using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Controls
{
    public sealed partial class AddFavoriteRecipeDialog : ContentDialog
    {
        public Action<RecipesAblum> OnAlbumTapped { get; set; }

        public AddFavoriteRecipeDialog()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.favoriteRecipe.OnAlbumTapped = album => 
            {
                if(OnAlbumTapped != null)
                {
                    OnAlbumTapped.Invoke(album);
                }
            };

            this.favoriteRecipe.CreateNewAlubmTapped = async () => 
            {
                this.Hide();
                AddNewAlbumDialog dialog = new AddNewAlbumDialog();
                dialog.ActionAfterAlbumCreated = async message => {
                    toast.Show(message);
                    await this.favoriteRecipe.LoadFirstPageDataAsync();
                };
                await dialog.ShowAsync();
                await this.ShowAsync();
            };

            this.favoriteRecipe.LoadFirstPageDataAsync();
        }

        private void hideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

    }
}
