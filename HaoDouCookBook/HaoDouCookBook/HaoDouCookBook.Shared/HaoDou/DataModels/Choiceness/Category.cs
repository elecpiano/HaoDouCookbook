using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{
    [DataContract]
    public class CategoryPageData
    {

        [DataMember(Name = "list")]
        public Category[] Categories { get; set; }

        public CategoryPageData()
        {

        }
    }


    [DataContract]
    public class Category
    {

        [DataMember]
        public string Cate { get; set; }


        [DataMember]
        public string ImgUrl { get; set; }


        [DataMember]
        public CategoryTag[] Tags { get; set; }

        public Category()
        {
            Cate = string.Empty;
            ImgUrl = string.Empty;
        }
    }



    [DataContract]
    public class CategoryTag
    {

        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string Name { get; set; }

        public CategoryTag()
        {
            Id = -1;
            Name = string.Empty;
        }
    }




}
