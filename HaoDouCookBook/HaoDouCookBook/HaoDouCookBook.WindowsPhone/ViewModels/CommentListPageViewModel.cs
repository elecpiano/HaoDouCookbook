﻿using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class CommentListPageViewModel: BindableBase
    {
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
        }

        public ObservableCollection<CommentListComment> Comments { get; set; }

        public CommentListPageViewModel()
        {
            userId = -1;
            image = string.Empty;
            Comments = new ObservableCollection<CommentListComment>();
        }
    }

    public class CommentListComment : BindableBase
    {
        private int cid;

        public int Cid
        {
            get { return cid; }
            set { SetProperty<int>(ref cid, value); }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }


        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private int atUserId;

        public int AtUserId
        {
            get { return atUserId; }
            set { SetProperty<int>(ref atUserId, value); }
        }

        private string atUserName;

        public string AtUserName
        {
            get { return atUserName; }
            set { SetProperty<string>(ref atUserName, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private int photoFlag;

        public int PhotoFlag
        {
            get { return photoFlag; }
            set { SetProperty<int>(ref photoFlag, value); }
        }

        private int photold;

        public int Photold
        {
            get { return photold; }
            set { SetProperty<int>(ref photold, value); }
        }

        private string photoUrl;

        public string PhotoUrl
        {
            get { return photoUrl; }
            set { SetProperty<string>(ref photoUrl, value); }
        }

        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { SetProperty<string>(ref createTime, value); }
        }

        public CommentListComment()
        {
            cid = -1;
            userId = -1;
            avatar = string.Empty;
            atUserId = -1;
            atUserName = string.Empty;
            userName = string.Empty;
            content = string.Empty;
            photoFlag = 0;
            photold = 0;
            photoUrl = string.Empty; 
            createTime = string.Empty;
        }
    }
}
