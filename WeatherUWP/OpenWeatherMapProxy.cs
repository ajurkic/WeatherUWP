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
    class OpenWeatherMapProxy
    {
        public async static Task<RootObject> GetWeather(string city)
        {
            var http = new HttpClient();
            //TODO: city name to id=3190261
            var response = await http.GetAsync("http://api.openweathermap.org/data/2.5/forecast?id=3190261&APPID=55532f27884ec31a83b68105ff4deb0f");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }
    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
        [DataMember]
        public double pressure { get; set; }
        [DataMember]
        public double sea_level { get; set; }
        [DataMember]
        public double grnd_level { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_kf { get; set; }
    }
    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }
    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }
    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }
        [DataMember]
        public double deg { get; set; }
    }

    /*
    [DataContract]
    public class Rain
    {
        [DataMember]
        public double? __invalid_name__3h { get; set; }
    }
    */

    [DataContract]
    public class Sys
    {
        [DataMember]
        public string pod { get; set; }
    }
    [DataContract]
    public class List
    {
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        /*[DataMember]
        public Rain rain { get; set; }*/
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public string dt_txt { get; set; }
    }
    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lon { get; set; }
    }
    [DataContract]
    public class City
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public string country { get; set; }
    }
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string cod { get; set; }
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public int cnt { get; set; }
        [DataMember]
        public List<List> list { get; set; }
        [DataMember]
        public City city { get; set; }
    }
}
