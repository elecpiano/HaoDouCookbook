using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{
    #region Albums

    [DataContract]
    public class FavoriteAlbumsData
    {
        [DataMember(Name = "list")]
        public FavoriteRecipesAlbum[] Albums { get; set; }

        public FavoriteAlbumsData()
        {

        }
    }

    /*
     *'Cid:Int'	=&gt; '专辑ID',
        Avatar'	=&gt; '作者图像',
	   'Cover:String'	=&gt; '专辑封面图片',
	    'Title:String'	=&gt; '专辑标题',
		'Status:Int'=&gt; '状态 1-审核 0-为审核',
		'Sys:Int'=&gt; '1 系统专辑 0 普通专辑',
		'RecipeCount' =&gt; '专辑下菜谱总数',
		'UserId' =&gt; '用户ID',
		'UserName' =&gt; '用户昵称',
		'Recipe:Array'=&gt; array(
			array(
				'Id:Int' =&gt; '菜谱ID',
				'RecipeId:Int' =&gt; '菜谱ID',
				'Cover:String' =&gt; '菜谱封面'
			),      
     */
    [DataContract]
    public class FavoriteRecipesAlbum
    {
        [DataMember]
        public int Cid { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public FavoriteRecipe[] Recipe { get; set; }

        [DataMember]
        public int RecipeCount { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public int Sys { get; set; }

        public FavoriteRecipesAlbum()
        {
            Cid = 0;
            Title = string.Empty;
            Cover = string.Empty;
            Status = 0;
            RecipeCount = 0;
            UserId = 0;
            UserName = string.Empty;
            Avatar = string.Empty;
            Sys = 0;
        }
    }


    [DataContract]
    public class FavoriteRecipe
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string Cover { get; set; }

        public FavoriteRecipe()
        {
            Id = 0;
            RecipeId = 0;
            Cover = string.Empty;
        }
    }

 
    #endregion

    #region Recipes

    [DataContract]
    public class FavoriteAblumsData
    {
        [DataMember(Name = "list")]
        public FavoriteAlbum[] Recipes { get; set; }

        public FavoriteAblumsData()
        {

        }
    }

    [DataContract]
    public class FavoriteAlbum
    {
        [DataMember]
        public int Cid { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string  Cover { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Contents { get; set; }

        public FavoriteAlbum()
        {
            Cid = 0;
            Title = string.Empty;
            Cover = string.Empty;
            Url = string.Empty;
            Contents = string.Empty;
        }
    }

    #endregion

    #region Topics

    [DataContract]
    public class FavoriteTopicsData
    {

        [DataMember(Name = "list")]
        public FavoriteTopic[] Topics { get; set; }

        public FavoriteTopicsData()
        {

        }
    }


    [DataContract]
    public class FavoriteTopic
    {
        [DataMember]
        public int Cid { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string  Cover { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Contents { get; set; }

        [DataMember]
        public int Type { get; set; }

        public FavoriteTopic()
        {
            Cid = 0;
            Type = 0;
            Title = string.Empty;
            Url = string.Empty;
            Contents = string.Empty;
        }
    }


    #endregion
}
