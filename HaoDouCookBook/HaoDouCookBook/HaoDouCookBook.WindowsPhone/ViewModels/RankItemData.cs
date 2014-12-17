using Shared.Infrastructures;
using Shared.Utility;

namespace HaoDouCookBook.ViewModels
{
    public class RankItemData : BindableBase, ILoadMoreItem
    {
        private string coverImage;

        public string CoverImage
        {
            get { return coverImage; }
            set { SetProperty<string>(ref coverImage, value); }
        }

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

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }


        public RankItemData()
        {
            coverImage = string.Empty;
            title = string.Empty;
            description = string.Empty;
            id = -1;
            type = -1;
            IsLoadMore = false;
        }

        public bool IsLoadMore { get; set; }
    }
}
