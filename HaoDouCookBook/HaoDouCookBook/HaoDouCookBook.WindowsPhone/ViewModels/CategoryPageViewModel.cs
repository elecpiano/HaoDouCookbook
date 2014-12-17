using Shared.Infrastructures;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class CategoryPageViewModel : BindableBase
    {
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryPageViewModel()
        {
            Categories = new ObservableCollection<Category>();
        }

    }

    public class Category : BindableBase
    {
        private string image;

        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        public List<CategoryTag> Tags { get; set; }

        public Category()
        {
            name = string.Empty;
            image = string.Empty;
            Tags = new List<CategoryTag>();
        }
    }

    public class CategoryTag : BindableBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        public CategoryTag()
        {
            id = -1;
            name = string.Empty;
        }
    }

}
