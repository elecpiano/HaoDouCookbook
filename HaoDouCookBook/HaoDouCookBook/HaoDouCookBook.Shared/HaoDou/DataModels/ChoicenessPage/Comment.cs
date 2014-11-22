using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    public class Comment
    {
        public int UserId { get; set; }
        public string Avatar { get; set; }

        public string UserName { get; set; }

        public int AtUserId { get; set; }

        public string AtUserName { get; set; }

        public string Content { get; set; }

        public string CreateTime { get; set; }

        public Comment()
        {
            UserId = - 1;
            AtUserId = -1;
            UserName = string.Empty;
            AtUserName = string.Empty;
            Content = string.Empty;
            CreateTime = string.Empty;
            Avatar = string.Empty;
        }

    }
}
