using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase
    {
        public ObservableCollection<StuffCategory> StuffCategories { get; set; }

        public ShoppingListPageViewModel()
        {
            StuffCategories = new ObservableCollection<StuffCategory>();
        }
    }


    public class StuffCategory : BindableBase
    {
        private int categoryId;

        public int CategoryId
        {
            get { return categoryId; }
            set { SetProperty<int>(ref categoryId, value); }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set { SetProperty<string>(ref category, value); }
        }

        public ObservableCollection<StuffItem> Stuffs { get; set; }

        public StuffCategory()
        {
            category = string.Empty;
            Stuffs = new ObservableCollection<StuffItem>();
        }
    }

    public class StuffItem : BindableBase
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

        public StuffItem()
        {

        }
    }


}
