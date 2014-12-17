using Shared.Infrastructures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class Feedback : BindableBase, ILoadMoreItem
    {
        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { SetProperty<string>(ref createTime, value); }
        }

        public bool IsLoadMore { get; set; }

        public Feedback()
        {
            content = string.Empty;
            createTime = string.Empty;
            IsLoadMore = false;
        }
    }

}
