using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;
using System.Linq;
using System.Text;
using Windows.Data.Json;
using Shared.Utility;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{
    public class DiscoveryPageData : Infrastructures.CustomJsonSerializableBase
    {
        public List<Actor> Actors { get; set; }

        public NewbieTutorial NewbieTutorial { get; set; }

        public List<Cate> Cates { get; set; }

        public StarredUser StarredUser { get; set; }

        public DailyMeal DailyMeal { get; set; }

        public DiscoveryPageData()
        {
            Actors = new List<Actor>();
            Cates = new List<Cate>();
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

                for (int i = 0; i < jarray.Count; i++)
                {
                    string type = jarray[i].GetObject()["ItemType"].GetString().Trim().ToLower();
                    switch (type)
                    {
                        case "day":
                            DailyMeal = JsonSerializer.Deserialize<DailyMeal>(jarray[i].Stringify());
                            break;
                        case "learn":
                            //NewbieTutorial = JsonSerializer.Deserialize<NewbieTutorial>(jarray[i].Stringify());
                            break;
                        case "cate":
                            Cate cate = JsonSerializer.Deserialize<Cate>(jarray[i].Stringify()); 
                            if(Cates.All(c => !c.TopicName.Equals(cate.TopicName, StringComparison.OrdinalIgnoreCase)))
                            {
                                Cates.Add(cate);
                            }
                            break;
                        case "user":
                            StarredUser = StarredUser = JsonSerializer.Deserialize<StarredUser>(jarray[i].Stringify());
                            break;
                        case "act":
                            Actor act =  JsonSerializer.Deserialize<Actor>(jarray[i].Stringify());
                            if(Actors.All(a => !a.OpenUrl.Equals(act.OpenUrl, StringComparison.OrdinalIgnoreCase)))
                            {
                                Actors.Add(act);
                            }
                            break;
                        default:
                            break;
                    }
                }
                return true;

            }
            catch
            {
                return false;
            }

        }
    }
}
