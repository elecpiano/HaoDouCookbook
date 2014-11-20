﻿using Shared.Infrastructures;
namespace HaoDouCookBook.ViewModels
{
    public class CategoryTileData : BindableBase
    {
        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set { SetProperty<string>(ref imageSource, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string id;

        public string Id
        {
            get { return id; }
            set { SetProperty<string>(ref id, value); }
        }


        public CategoryTileData()
        {
            title = string.Empty;
            imageSource = string.Empty;
            id = "-1";
        }
    }
}
