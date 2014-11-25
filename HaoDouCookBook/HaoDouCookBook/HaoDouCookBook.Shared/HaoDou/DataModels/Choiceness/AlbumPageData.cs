using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class AlbumPageData
    {
        [DataMember(Name = "info")]
        public AlbumInfo Info { get; set; }

        [DataMember(Name = "list")]
        public AlbumPageRecipe[] Recipes { get; set; }

    }

    [DataContract]
    public class AlbumInfo
    {
        [DataMember]
        public string AlbumTitle { get; set; }

        [DataMember]
        public string AlbumAvatarUrl { get; set; }

        [DataMember]
        public string AlbumUserName { get; set; }

        [DataMember]
        public int AlbumUserId { get; set; }

        [DataMember]
        public string AlbumContent { get; set; }

        [DataMember]
        public string AlbumCover { get; set; }

        [DataMember]
        public int AlbumIsLike { get; set; }

        [DataMember]
        public string CommentCount { get; set; }

        public AlbumInfo()
        {
            AlbumTitle = string.Empty;
            AlbumAvatarUrl = string.Empty;
            AlbumUserName = string.Empty;
            AlbumUserId = -1;
            AlbumContent = string.Empty;
            AlbumCover = string.Empty;
            AlbumIsLike = 0;
            CommentCount = "0";
        }
    }


    [DataContract]
    public class AlbumPageRecipe
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int LikeCount { get; set; }

        [DataMember]
        public int ViewCount { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string Stuff { get; set; }

        [DataMember]
        public int IsLike { get; set; }

        public AlbumPageRecipe()
        {
            UserId = -1;
            Title = string.Empty;
            Cover = string.Empty;
            LikeCount = 0;
            ViewCount = 0;
            RecipeId = -1;
            UserName = string.Empty;
            Intro = string.Empty;
            Stuff = string.Empty;
            IsLike = 0;
        }
    }
}
