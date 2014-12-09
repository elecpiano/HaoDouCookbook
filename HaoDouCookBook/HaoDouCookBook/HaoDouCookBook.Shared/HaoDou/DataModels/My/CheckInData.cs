using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class CheckinData
    {

        [DataMember(Name = "errormsg")]
        public string Message { get; set; }

        [DataMember]
        public int Wealth { get; set; }

        [DataMember]
        public int Add { get; set; }

        [DataMember]
        public int Days { get; set; }

        [DataMember]
        public int NextDay { get; set; }

        public CheckinData()
        {

            Message = string.Empty;
            Wealth = 0;
            Add = 0;
            Days = 0;
            NextDay = 0;
        }
    }

}
