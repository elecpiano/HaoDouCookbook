using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels
{
    [DataContract]
    public class HaodouResultMessage
    {
        [DataMember(Name = "errormsg")]
        public string Message { get; set; } 
    }
}
