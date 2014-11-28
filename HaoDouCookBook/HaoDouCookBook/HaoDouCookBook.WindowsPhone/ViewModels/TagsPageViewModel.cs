using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class TagsPageViewModel : BindableBase
    {
        public ObservableCollection<TagRecipeData> Recipes { get; set; }

        public TagsPageViewModel()
        {
            Recipes = new ObservableCollection<TagRecipeData>();
        }
    }
}
