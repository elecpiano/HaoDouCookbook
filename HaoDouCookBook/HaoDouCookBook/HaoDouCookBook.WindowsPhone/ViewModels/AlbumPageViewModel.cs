using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class AlbumPageViewModel : BindableBase
    {
        private string albumTitle;

        public string AlbumTitle
        {
            get { return albumTitle; }
            set { SetProperty<string>(ref albumTitle, value); }
        }

        private string albumAvatar;

        public string AlbumAvatar
        {
            get { return albumAvatar; }
            set { SetProperty<string>(ref albumAvatar, value); }
        }


        private string albumUserName;

        public string AlbumUserName
        {
            get { return albumUserName; }
            set { SetProperty<string>(ref albumUserName, value); }
        }


        private int albumUserId;

        public int AlbumUserId
        {
            get { return albumUserId; }
            set { SetProperty<int>(ref albumUserId, value); }
        }

        private string albumContent;

        public string AlbumContent
        {
            get { return albumContent; }
            set { SetProperty<string>(ref albumContent, value); }
        }


        private string albumCover;

        public string AlbumCover
        {
            get { return albumCover; }
            set { SetProperty<string>(ref albumCover, value); }
        }


        private bool albumIsLike;

        public bool AlbumIsLike
        {
            get { return albumIsLike; }
            set { SetProperty<bool>(ref albumIsLike, value); }
        }


        private int commentCount;

        public int CommentCount
        {
            get { return commentCount; }
            set { SetProperty<int>(ref commentCount, value); }
        }


        public ObservableCollection<RecipeTileData> Recipes { get; set; }

        public AlbumPageViewModel()
        {
            albumAvatar = string.Empty;
            albumContent = string.Empty;
            albumCover = string.Empty;
            albumIsLike = false;
            albumTitle = string.Empty;
            albumUserId = -1;
            albumUserName = string.Empty;
            Recipes = new ObservableCollection<RecipeTileData>();
        }
    }
}
