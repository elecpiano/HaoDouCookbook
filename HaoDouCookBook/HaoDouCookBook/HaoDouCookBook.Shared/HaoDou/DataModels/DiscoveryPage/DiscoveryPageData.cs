using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using Windows.Data.Json;
using Shared.Utility;

namespace HaoDouCookBook.HaoDou.DataModels.DiscoveryPage
{
    [DataContract]
    public class DiscoveryPageData : Infrastructures.CustomJsonSerializableBase
    {
        [DataMember]
        public Actor Actor { get; set; }

        public NewbieTutorial NewbieTutorial { get; set; }

        [DataMember]
        public Cate CateOne { get; set; }

        [DataMember]
        public Cate CateTwo { get; set; }

        [DataMember]
        public StarredUser StarredUser { get; set; }

        [DataMember]
        public DailyMeal DailyMeal { get; set; }

        public DiscoveryPageData()
        {
            Actor = new Actor();
            CateTwo = new Cate();
            CateOne = new Cate();
            StarredUser = new StarredUser();
            DailyMeal = new DailyMeal();
            NewbieTutorial = new NewbieTutorial();
        }

        public override bool Deserialize(string json)
        {
            try
            {
                JsonObject jsonObj = JsonObject.Parse(json);
                JsonArray jarray = jsonObj.GetNamedArray("list");

                DailyMeal = JsonSerializer.Deserialize<DailyMeal>(jarray[0].Stringify());
                Actor = JsonSerializer.Deserialize<Actor>(jarray[1].Stringify());
                NewbieTutorial = JsonSerializer.Deserialize<NewbieTutorial>(jarray[2].Stringify());
                CateOne = JsonSerializer.Deserialize<Cate>(jarray[3].Stringify());
                CateTwo = JsonSerializer.Deserialize<Cate>(jarray[4].Stringify());
                StarredUser = JsonSerializer.Deserialize<StarredUser>(jarray[5].Stringify());

                return true;

            }
            catch
            {
                return false;
            }

        }
    }
}
