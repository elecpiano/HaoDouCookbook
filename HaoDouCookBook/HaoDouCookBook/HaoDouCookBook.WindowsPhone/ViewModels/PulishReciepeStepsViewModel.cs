using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class RecipeStep : BindableBase
    {
        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty<string>(ref description, value); }
        }


        private string photo;

        public string Photo
        {
            get { return photo; }
            set { SetProperty<string>(ref photo, value); }
        }


        public RecipeStep()
        {
            description = string.Empty;
            photo = string.Empty;
        }
    }

}
