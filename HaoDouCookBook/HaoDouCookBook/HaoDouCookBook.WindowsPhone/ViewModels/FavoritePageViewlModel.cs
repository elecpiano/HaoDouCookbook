using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class FavoritePageViewlModel : BindableBase
    {
        public FavoritePageViewlModel()
        {

        }
    }

    public class FavoriteAlbumItem : BindableBase
    {
        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        
        private int albumId;

        public int AlbumId
        {
            get { return albumId; }
            set { SetProperty<int>(ref albumId, value); }
        }


        public FavoriteAlbumItem()
        {
            albumId = 0;
            cover = string.Empty;
            title = string.Empty;
            intro = string.Empty;
        }
    }

    public class FavoriteTopicItem : BindableBase
    {
        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private int topicId;

        public int TopicId
        {
            get { return topicId; }
            set { SetProperty<int>(ref topicId, value); }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }


        public FavoriteTopicItem()
        {
            cover = string.Empty;
            title = string.Empty;
            intro = string.Empty;
            topicId = 0;
            url = string.Empty;
        }

    }

}
