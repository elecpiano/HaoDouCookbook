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

        private int likeCount;

        public int LikeCount
        {
            get { return likeCount; }
            set { SetProperty<int>(ref likeCount, value); }
        }

        private TopicModel  topic;

        public TopicModel  Topic
        {
            get { return topic; }
            set { SetProperty<TopicModel >(ref topic, value); }
        }

        public SearchResultPageViewModel()
        {

        }
    }

}
