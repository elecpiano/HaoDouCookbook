using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class TopicModel : BindableBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }

        private string topicPreviewImageSource;

        public string TopicPreviewImageSource
        {
            get { return topicPreviewImageSource; }
            set { SetProperty<string>(ref topicPreviewImageSource, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set { SetProperty<string>(ref author, value); }
        }

        private string previewContent;

        public string PreviewContent
        {
            get { return previewContent; }
            set { SetProperty<string>(ref previewContent, value); }
        }

        private string createTimeDescription;

        public string CreateTimeDescription
        {
            get { return createTimeDescription; }
            set { SetProperty<string>(ref createTimeDescription, value); }
        }

        public TopicModel()
        {
            id = 0;
            url = string.Empty;
        }
    }
}
