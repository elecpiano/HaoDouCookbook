using HaoDouCookBook.Common;
using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HaoDouCookBook.ViewModels
{
    public class NewbieTutorial : BindableBase
    {
        private string mainImageSource;

        public string MainImageSource
        {
            get { return mainImageSource; }
            set { SetProperty<string>(ref mainImageSource, value); }
        }

        private UserData teacher;

        public UserData Teacher
        {
            get { return teacher; }
            set { SetProperty<UserData>(ref teacher, value); }
        }

        public string DishName { get; set; }

        public ObservableCollection<string> DetailsImageSources { get; set; }

        public NewbieTutorial()
        {
            mainImageSource = Constants.DEFAULT_TOPIC_IMAGE;
            teacher = new UserData();
            DetailsImageSources = new ObservableCollection<string>();
            DishName = string.Empty;
        }
        
    }
}
