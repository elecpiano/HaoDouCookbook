using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class UserRecipeOrProductBase : BindableBase
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

        

        public UserRecipeOrProductBase()
        {
            id = 0;
            cover = string.Empty;
        }
    }

    public class UserRecipe : UserRecipeOrProductBase
    {
 
    }

    public class UserProduct : UserRecipeOrProductBase
    {
 
    }

}
