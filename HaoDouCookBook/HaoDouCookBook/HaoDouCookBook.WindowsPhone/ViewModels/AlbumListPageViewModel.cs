using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class AlbumListPageViewModel : BindableBase
    {
        private int count;

        public int Count
        {
            get { return count; }
            set { SetProperty<int>(ref count, value); }
        }

        public ObservableCollection<AlbumTile> AlbumItems { get; set; }

        public AlbumListPageViewModel()
        {
            count = 0;
            AlbumItems = new ObservableCollection<AlbumTile>();
        }
    }


    public class AlbumTile : BindableBase
    {

        private int albumId;

        public int AlbumId
        {
            get { return albumId; }
            set { SetProperty<int>(ref albumId, value); }
        }

        private string albumTitle;

        public string AlbumTitle
        {
            get { return albumTitle; }
            set { SetProperty<string>(ref albumTitle, value); }
        }

        private string albumIntro;

        public string AlbumIntro
        {
            get { return albumIntro; }
            set { SetProperty<string>(ref albumIntro, value); }
        }

        private int albumViewCount;

        public int AlbumViewCount
        {
            get { return albumViewCount; }
            set { SetProperty<int>(ref albumViewCount, value); }
        }

        private int albumRecipeCount;

        public int AlbumRecipeCount
        {
            get { return albumRecipeCount; }
            set { SetProperty<int>(ref albumRecipeCount, value); }
        }

        private string albumCover;

        public string AlbumCover
        {
            get { return albumCover; }
            set { SetProperty<string>(ref albumCover, value); }
        }

        private int albumlikeCount;

        public int AlbumLikeCount
        {
            get { return albumlikeCount; }
            set { SetProperty<int>(ref albumlikeCount, value); }
        }


        public AlbumTile()
        {
            albumRecipeCount = 0;
            albumlikeCount = 0;
            albumViewCount = 0;
            albumCover = string.Empty;
            albumId = -1;
            albumIntro = string.Empty;
            albumTitle = string.Empty;
        }
    }

}
