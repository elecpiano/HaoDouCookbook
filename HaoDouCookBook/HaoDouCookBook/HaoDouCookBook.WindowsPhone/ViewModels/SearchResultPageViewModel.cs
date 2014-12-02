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

        private AlbumTile album;

        public AlbumTile Album
        {
            get { return album; }
            set { SetProperty<AlbumTile>(ref album, value); }
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
            album = new AlbumTile();
            topic = new TopicModel();
            Recipes = new ObservableCollection<TagRecipeData>();
        }
    }
}
