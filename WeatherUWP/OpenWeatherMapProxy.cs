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

    //nać to da mi api vrati listu gradova koji počinju s određenim stringom
    //flow layout uwp
    //čitat samo id i name, a ne sve

    class OpenWeatherMapProxy
    {
        public async static Task<RootObject> GetWeather(string city)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("http://api.apixu.com/v1/current.json?key=e761b396dc064e60ab6132722170305&q=" + city);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }
    [DataContract]
    public class Location
    {
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
        public string tz_id { get; set; }
        [DataMember]
        public int localtime_epoch { get; set; }
        [DataMember]
        public string localtime { get; set; }
    }
    [DataContract]
    public class Condition
    {
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public string icon { get; set; }
        [DataMember]
        public int code { get; set; }
    }
    [DataContract]
    public class Current
    {
        [DataMember]
        public int last_updated_epoch { get; set; }
        [DataMember]
        public string last_updated { get; set; }
        [DataMember]
        public double temp_c { get; set; }
        [DataMember]
        public double temp_f { get; set; }
        [DataMember]
        public int is_day { get; set; }
        [DataMember]
        public Condition condition { get; set; }
        [DataMember]
        public double wind_mph { get; set; }
        [DataMember]
        public double wind_kph { get; set; }
        [DataMember]
        public int wind_degree { get; set; }
        [DataMember]
        public string wind_dir { get; set; }
        [DataMember]
        public double pressure_mb { get; set; }
        [DataMember]
        public double pressure_in { get; set; }
        [DataMember]
        public double precip_mm { get; set; }
        [DataMember]
        public double precip_in { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public int cloud { get; set; }
        [DataMember]
        public double feelslike_c { get; set; }
        [DataMember]
        public double feelslike_f { get; set; }
        [DataMember]
        public double vis_km { get; set; }
        [DataMember]
        public double vis_miles { get; set; }
    }
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public Current current { get; set; }
    }
}
