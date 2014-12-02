using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class ShakePageViewModel : BindableBase
    {
        public ObservableCollection<TagRecipeData> Recipes { get; set; }

        public ShakePageViewModel()
        {
            Recipes = new ObservableCollection<TagRecipeData>();
        }
    }

}
