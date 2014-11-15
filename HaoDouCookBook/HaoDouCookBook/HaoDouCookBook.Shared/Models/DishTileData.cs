using Shared.Infrastructures;

namespace HaoDouCookBook.Models
{
    public class DishTileData : BindableBase
    {
        private string dishImageSource;

        public string DishImageSource
        {
            get { return dishImageSource; }
            set { SetProperty<string>(ref dishImageSource, value); }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set { SetProperty<string>(ref author, value); }
        }

        private int supportNumber;

        public int SupportNumber
        {
            get { return supportNumber; }
            set { SetProperty<int>(ref supportNumber, value); }
        }

        public DishTileData()
        {
            dishImageSource = string.Empty;
            author = string.Empty;
            supportNumber = 0;
        }
        
    }
}
