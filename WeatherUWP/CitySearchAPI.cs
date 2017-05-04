using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUWP
{
    /*Sta na tome da triba sad riješit autosuggestbox_textchanged funkciju i praktički to je to
        Onda ostaje samo uredit sučelje i napravit happy test :D */
    class CitySearchAPI
    {
        public async static Task<CityRootObject> SearchCity(string city)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("http://api.apixu.com/v1/search.json?key=e761b396dc064e60ab6132722170305&q=" + city);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(CityRootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var data = (CityRootObject)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class CityRootObject
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string region { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public string url { get; set; }
    }
}
