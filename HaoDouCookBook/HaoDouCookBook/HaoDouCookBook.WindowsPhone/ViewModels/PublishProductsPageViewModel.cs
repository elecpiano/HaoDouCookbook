using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class PublishProductsPageViewModel : BindableBase
    {
        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string position;

        public string Position
        {
            get { return position; }
            set { SetProperty<string>(ref position, value); }
        }

        private int topicTagId;

        public int TopicTagId
        {
            get { return topicTagId; }
            set { SetProperty<int>(ref topicTagId, value); }
        }

        private string topicTagName;

        public string TopicTagName
        {
            get { return topicTagName; }
            set { SetProperty<string>(ref topicTagName, value); }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { SetProperty<string>(ref city, value); }
        }

        public ObservableCollection<TopicTag> Tags { get; set; }

        public PublishProductsPageViewModel()
        {
            intro = string.Empty;
            name = string.Empty;
            cover = string.Empty;
            position = string.Empty;
            topicTagId = 0;
            topicTagName = string.Empty;
            city = string.Empty;
            Tags = new ObservableCollection<TopicTag>();
        }
    }

    public class TopicTag : BindableBase
    {
        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set { SetProperty<bool>(ref selected, value); }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { SetProperty<string>(ref text, value); }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        public TopicTag()
        {
            selected = false;
            text = string.Empty;
        }
    }

}
