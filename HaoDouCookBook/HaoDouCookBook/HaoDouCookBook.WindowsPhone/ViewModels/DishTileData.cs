using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class DishTileData : BindableBase
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { SetProperty<int>(ref productId, value); }
        }
        
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

        private int dishImageHeight;

        public int DishImageHeight
        {
            get { return dishImageHeight; }
            set { SetProperty<int>(ref dishImageHeight, value); }
        }

        private int dishImageWidth;

        public int DishImageWidth
        {
            get { return dishImageWidth; }
            set { SetProperty<int>(ref dishImageWidth, value); }
        }


        public DishTileData()
        {
            dishImageSource = string.Empty;
            author = string.Empty;
            supportNumber = 0;
            dishImageHeight = 180;
            dishImageWidth = 170;
        }

    }
}
