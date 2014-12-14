using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class Areas
    {
        [DataMember(Name = "list")]
        public Province[] Provinces { get; set; }


        public Areas()
        {

        }
    }



    [DataContract]
    public class Province
    {
        [DataMember(Name = "Citys")]
        public City[] Cities { get; set; }

        [DataMember]
        public int ProvinceId { get; set; }

        [DataMember]
        public string ProvinceName { get; set; }

        public Province()
        {
            ProvinceId = 0;
            ProvinceName = string.Empty;
        }
    }


    [DataContract]
    public class City
    {
        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public string CityName { get; set; }

        public City()
        {
            CityId = 0;
            CityName = string.Empty;
        }
    }

}
