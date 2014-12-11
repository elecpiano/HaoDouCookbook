using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class PublishRecipePageViewModel : BindableBase
    {
        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set
            {
                ContentChanged = true;
                SetProperty<string>(ref recipeName, value);
            }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string skills;

        public string Skills
        {
            get { return skills; }
            set { SetProperty<string>(ref skills, value); }
        }

        public bool ContentChanged { get; set; }

        public ObservableCollection<StuffItem> MainStuffs { get; set; }

        public ObservableCollection<StuffItem> OtherStuffs { get; set; }

        public PublishRecipePageViewModel()
        {
            MainStuffs = new ObservableCollection<StuffItem>();
            MainStuffs.Add(new StuffItem());
            MainStuffs.CollectionChanged += (s, e) => { ContentChanged = true; };
            OtherStuffs = new ObservableCollection<StuffItem>();
            OtherStuffs.Add(new StuffItem());
            OtherStuffs.CollectionChanged += (s, e) => { ContentChanged = true; };
            ContentChanged = false;
        }
    }
}
