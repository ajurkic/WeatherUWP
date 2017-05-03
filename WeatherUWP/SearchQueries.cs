using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace WeatherUWP
{
    class SearchQueries
    {
        List<Cities> listOfCities = new List<Cities>();

        public List<Cities> GetAllCities(string city)
        {
            return listOfCities;
        }

        public async void SetCities()
        {
            listOfCities.Clear();

            Uri dataUri = new Uri("ms-apps:///CityData/city.list.json");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);

            JsonArray jsonArray = jsonObject["name"].GetArray(); //jsonObject["cities"].GetArray();

            foreach(var item in jsonArray)
            {
                //Ode bi triba provjerit postoji li grad unutar liste, da ih ne moran sve učitavat
                //Ili da ih učitavan samo po slovu šta korisnik unese???

                JsonObject groupObject = item.GetObject();
                listOfCities.Add(new Cities() { name = groupObject.GetNamedString("name") });
            }
        }

        public SearchQueries()
        {
            SetCities();
        }
    }
}
