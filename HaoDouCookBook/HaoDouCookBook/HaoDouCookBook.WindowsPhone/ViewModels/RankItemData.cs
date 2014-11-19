using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class RankItemData : BindableBase
    {
        private string coverImage;

        public string CoverImage
        {
            get { return coverImage; }
            set { SetProperty<string>(ref coverImage, value); }
        }

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

        public RankItemData()
        {
            coverImage = string.Empty;
            title = string.Empty;
            description = string.Empty;
        }

    }
}
