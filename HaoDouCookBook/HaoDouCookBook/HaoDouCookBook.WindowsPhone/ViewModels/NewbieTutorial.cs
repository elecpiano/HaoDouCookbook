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

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string openUrl;

        public string OpenUrl
        {
            get { return openUrl; }
            set { SetProperty<string>(ref openUrl, value); }
        }


        public ObservableCollection<string> DetailsImageSources { get; set; }

        public NewbieTutorial()
        {
            mainImageSource = string.Empty;
            teacher = new UserData();
            DetailsImageSources = new ObservableCollection<string>();
            Title = string.Empty;
        }
        
    }
}
