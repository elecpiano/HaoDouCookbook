using Shared.Infrastructures;
namespace HaoDouCookBook.Models
{
    public class CategoryTileData : BindableBase
    {
        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set { SetProperty<string>(ref imageSource, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        public CategoryTileData()
        {
            title = string.Empty;
            imageSource = string.Empty;
        }
    }
}
