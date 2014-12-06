using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class AdRecommendationPageViewModel : BindableBase
    {
        public ObservableCollection<AdRecommendationItem> AdItems { get; set; }

        public AdRecommendationPageViewModel()
        {
            AdItems = new ObservableCollection<AdRecommendationItem>();
        }

    }

    public class AdRecommendationItem : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty<string>(ref description, value); }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }


        public AdRecommendationItem()
        {
            title = string.Empty;
            description = string.Empty;
            image = string.Empty;
            url = string.Empty;
        }
    }

}
