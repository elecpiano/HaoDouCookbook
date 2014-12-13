using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class PersonalTagsData
    {

        [DataMember(Name = "list")]
        public PersonalTag[] Tags { get; set; }

        public PersonalTagsData()
        {

        }
    }


    [DataContract]
    public class PersonalTag
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Img { get; set; }

        [DataMember]
        public bool Check { get; set; }

        public PersonalTag()
        {
            Id = 0;
            Name = string.Empty;
            Img = string.Empty;
            Check = false;
        }
    }


}
