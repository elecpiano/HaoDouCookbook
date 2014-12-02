using HaoDouCookBook.Common;
using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class SpecialRecipeAlbumData : BindableBase
    {
         private string mainImageSource;

        public string MainImageSource
        {
            get { return mainImageSource; }
            set { SetProperty<string>(ref mainImageSource, value); }
        }


        public ObservableCollection<string> DetailsImageSources { get; set; }

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

        private string albumMarkImageSource;

        public string AlbumMarkImageSource
        {
            get { return albumMarkImageSource; }
            set { SetProperty<string>(ref albumMarkImageSource, value); }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }


        public SpecialRecipeAlbumData()
        {
            albumMarkImageSource = string.Empty;
            mainImageSource = Constants.DEFAULT_TOPIC_IMAGE;
            DetailsImageSources = new ObservableCollection<string>();
            title = string.Empty;
            description = string.Empty;
            id = -1;
        }
    }
}
