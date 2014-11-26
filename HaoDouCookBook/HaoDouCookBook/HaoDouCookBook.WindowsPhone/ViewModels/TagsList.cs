using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class TagItem : BindableBase
    {
        private int? id;

        public int? Id
        {
            get { return id; }
            set { SetProperty<int?>(ref id, value); }
        }


        private string icon;

        public string Icon
        {
            get { return icon; }
            set { SetProperty<string>(ref icon, value); }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { SetProperty<string>(ref text, value); }
        }

        public TagItem()
        {
            icon = string.Empty;
            text = string.Empty;
        }
        
    }

    public class TagsListData : BindableBase
    {
        private TagItem tag1;

        public TagItem Tag1
        {
            get { return tag1; }
            set { SetProperty<TagItem>(ref tag1, value); }
        }

        private TagItem tag2;

        public TagItem Tag2
        {
            get { return tag2; }
            set { SetProperty<TagItem>(ref tag2, value); }
        }

        private TagItem tag3;

        public TagItem Tag3
        {
            get { return tag3; }
            set { SetProperty<TagItem>(ref tag3, value); }
        }

        private TagItem tag4;

        public TagItem Tag4
        {
            get { return tag4; }
            set { SetProperty<TagItem>(ref tag4, value); }
        }

        private TagItem tag5;

        public TagItem Tag5
        {
            get { return tag5; }
            set { SetProperty<TagItem>(ref tag5, value); }
        }

        private TagItem tag6;

        public TagItem Tag6
        {
            get { return tag6; }
            set { SetProperty<TagItem>(ref tag6, value); }
        }

        private TagItem tag7;

        public TagItem Tag7
        {
            get { return tag7; }
            set { SetProperty<TagItem>(ref tag7, value); }
        }

        private TagItem tag8;

        public TagItem Tag8
        {
            get { return tag8; }
            set { SetProperty<TagItem>(ref tag8, value); }
        }

        public TagsListData()
        {
            tag1 = new TagItem();
            tag2 = new TagItem();
            tag3 = new TagItem();
            tag4 = new TagItem();
            tag5 = new TagItem();
            tag6 = new TagItem();
            tag7 = new TagItem();
            tag8 = new TagItem();
        }
    }
}
