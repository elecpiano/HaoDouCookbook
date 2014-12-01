using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{
    [DataContract]
    public class SearchPageData
    {
        [DataMember(Name = "list")]
        public string[] HotSearchKeywords { get; set; }

        public SearchPageData()
        {

        }
    }


    [DataContract]
    public class SearchSuggestionData
    {

        [DataMember(Name = "list")]
        public string[] Keywords { get; set; }

        public SearchSuggestionData()
        {

        }
    }



    [DataContract]
    public class SearchResultPageData
    {

        [DataMember(Name = "food")]
        public FoodInfo Food { get; set; }


        [DataMember(Name = "recipe")]
        public TagsPageData Recipes { get; set; }


        [DataMember]
        public AlbumPageData Album { get; set; }


        [DataMember]
        public SearchResultPageTopicsData TopicData { get; set; }


        public SearchResultPageData()
        {

        }
    }


    [DataContract]
    public class SearchResultPageTopicsData
    {

        [DataMember(Name = "list")]
        public SearchResultPageTopic[] Topics { get; set; }

        public SearchResultPageTopicsData()
        {

        }
    }

    

    [DataContract]
    public class SearchResultPageTopic
    {

        [DataMember]
        public int TopicId { get; set; }


        [DataMember]
        public string Title { get; set; }


        [DataMember]
        public string Intro { get; set; }


        [DataMember]
        public string Cover { get; set; }


        [DataMember]
        public string UserName { get; set; }


        [DataMember]
        public string CreateTime { get; set; }


        [DataMember]
        public string Url { get; set; }

        public SearchResultPageTopic()
        {
            TopicId = -1;
            Title = string.Empty;
            Intro = string.Empty;
            Cover = string.Empty;
            UserName = string.Empty;
            CreateTime = string.Empty;
            Url = string.Empty;
        }
    }


}
