using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class SearchResultPageViewModel : BindableBase
    {
        private Food food;

        public Food Food
        {
            get { return food; }
            set { SetProperty<Food>(ref food, value); }
        }


        public ObservableCollection<TagRecipeData> Recipes { get; set; }

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

        private TopicModel  topic;

        public TopicModel  Topic
        {
            get { return topic; }
            set { SetProperty<TopicModel >(ref topic, value); }
        }

        public SearchResultPageViewModel()
        {

            food = new Food();
            albumId = -1;
            albumTitle = string.Empty;
            albumCover = string.Empty;
            albumRecipeCount = 0;
            albumViewCount = 0;
            albumlikeCount = 0;
            topic = new TopicModel();
            Recipes = new ObservableCollection<TagRecipeData>();
        }
    }

}
