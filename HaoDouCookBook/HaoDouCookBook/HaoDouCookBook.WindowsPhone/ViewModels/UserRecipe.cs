using Shared.Infrastructures;
using Shared.Utility;

namespace HaoDouCookBook.ViewModels
{
    public class UserRecipeOrProductBase : BindableBase, ILoadMoreItem
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }
        
        public bool IsLoadMore { get; set; }

        public UserRecipeOrProductBase()
        {
            id = 0;
            cover = string.Empty;
            IsLoadMore = false;
        }
    }

    public class UserRecipe : UserRecipeOrProductBase
    {
 
    }

    public class UserProduct : UserRecipeOrProductBase
    {
 
    }

}
