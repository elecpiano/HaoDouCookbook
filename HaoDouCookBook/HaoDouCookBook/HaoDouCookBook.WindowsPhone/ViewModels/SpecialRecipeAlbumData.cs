using HaoDouCookBook.Common;
using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class SpecialRecipeAlbumData : BindableBase
    {
         private string mainImageSource;

        public string MainImageSource
        {
            get { return mainImageSource; }
            set { SetProperty<string>(ref mainImageSource, value); }
        }


        public ObservableCollection<string> DetailsImageSources { get; set; }

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
           

        public SpecialRecipeAlbumData()
        {
            mainImageSource = Constants.DEFAULT_TOPIC_IMAGE;
            DetailsImageSources = new ObservableCollection<string>();
            title = string.Empty;
            description = string.Empty;
        }
    }
}
