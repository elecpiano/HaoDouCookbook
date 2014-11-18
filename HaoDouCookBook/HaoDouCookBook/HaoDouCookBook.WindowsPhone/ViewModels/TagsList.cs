using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class TagsListData : BindableBase
    {
        private string tag1;

        public string Tag1
        {
            get { return tag1; }
            set { SetProperty<string>(ref tag1, value); }
        }

        private string tag2;

        public string Tag2
        {
            get { return tag2; }
            set { SetProperty<string>(ref tag2, value); }
        }

        private string tag3;

        public string Tag3
        {
            get { return tag3; }
            set { SetProperty<string>(ref tag3, value); }
        }

        private string tag4;

        public string Tag4
        {
            get { return tag4; }
            set { SetProperty<string>(ref tag4, value); }
        }

        private string tag5;

        public string Tag5
        {
            get { return tag5; }
            set { SetProperty<string>(ref tag5, value); }
        }

        private string tag6;

        public string Tag6
        {
            get { return tag6; }
            set { SetProperty<string>(ref tag6, value); }
        }

        private string tag7;

        public string Tag7
        {
            get { return tag7; }
            set { SetProperty<string>(ref tag7, value); }
        }

        private string tag8;

        public string Tag8
        {
            get { return tag8; }
            set { SetProperty<string>(ref tag8, value); }
        }

        public TagsListData()
        {
            tag1 = string.Empty;
            tag2 = string.Empty;
            tag3 = string.Empty;
            tag4 = string.Empty;
            tag5 = string.Empty;
            tag6 = string.Empty;
            tag7 = string.Empty;
            tag8 = string.Empty;
        }
    }
}
