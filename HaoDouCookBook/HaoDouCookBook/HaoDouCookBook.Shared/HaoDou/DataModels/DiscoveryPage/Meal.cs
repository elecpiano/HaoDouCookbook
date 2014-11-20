using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.DiscoveryPage
{
    [DataContract]
    public class Meal
    {
        [DataMember]
        public int PhotoCount { get; set; }

        [DataMember]
        public int PhotoFixCount { get; set; }

        [DataMember]
        public int Pid { get; set; }

        [DataMember]
        public int TopicId { get; set; }

        [DataMember]
        public string ThemeTitle { get; set; }

        [DataMember]
        public string ThemeCover { get; set; }

        [DataMember]
        public string OpenUrl { get; set; }

        public Meal()
        {
            TopicId = -1;
            Pid = -1;
            ThemeTitle = string.Empty;
            ThemeCover = string.Empty;
            OpenUrl = string.Empty;
        }
    }

    [DataContract]
    public class DailyMeal
    {
        [DataMember]
        public string ItemType { get; set; }

        [DataMember(Name = "List")]
        public Meal[] Meals { get; set; }

        public DailyMeal()
        {
            ItemType = string.Empty;
        }
    }
}
