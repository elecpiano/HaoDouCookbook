using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels
{
    [DataContract]
    public class HaodouResultMessage
    {
        [DataMember(Name = "errormsg")]
        public string Message { get; set; } 
    }
}
