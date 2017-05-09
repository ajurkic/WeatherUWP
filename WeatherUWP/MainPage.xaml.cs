using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        List<String> cityNames = new List<string>();

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.QueryText != "")
            {
                RootObject myWeather = await WeatherAPI.GetWeather(args.QueryText);

                if (myWeather.current.condition.code != 400 || 
                    myWeather.current.condition.code != 401 ||
                    myWeather.current.condition.code != 403)
                {
                    TitleTextBox.Text = String.Format("Weather in {0}", myWeather.location.name);
                    TemperatureTextBox.Text = myWeather.current.temp_c.ToString();
                    feelsLikeTextBox.Text = myWeather.current.feelslike_c.ToString();
                    WindTextBox.Text = myWeather.current.wind_kph.ToString() + " km/h";
                    HumidityTextBox.Text = myWeather.current.humidity.ToString();
                    CloudinessTextBox.Text = myWeather.current.cloud.ToString() + " %";
                    CloudinessProgressBar.Value = myWeather.current.cloud;
                    PressureTextBox.Text = myWeather.current.pressure_mb + " hPa";
                    DescriptionTextBox.Text = myWeather.current.condition.text;
                    
                    string icon = String.Format("http:{0}", myWeather.current.condition.icon);
                    ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                }
                else
                {
                    DescriptionTextBox.Text = "Error: Cannot get the forecast. Check your internet connection";
                }
            }
        }

        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && sender.Text != "" && sender.Text != " ")
            {
                cityNames.Clear();
                string uri = "http://api.apixu.com/v1/search.json?key=e761b396dc064e60ab6132722170305&q=" + sender.Text;
                HttpClient http = new HttpClient();
                string response = await http.GetStringAsync(uri);
                
                var cities = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CitySearchAPI>>(response);
                
                if (cities.Count() != 0)
                {
                    foreach (var city in cities)
                    {
                        cityNames.Add(city.name);
                    }
                    sender.ItemsSource = cityNames;
                }
                else
                {
                    sender.ItemsSource = new string[] { "City not found" };
                }
            }
        }



        //Pitat prof jel može kako drukčije?
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RootObject myWeather = await WeatherAPI.GetWeather("Split");
            
            if (myWeather.current.condition.code != 400 ||
                myWeather.current.condition.code != 401 ||
                myWeather.current.condition.code != 403)
            {
                TitleTextBox.Text = String.Format("Weather in {0}", myWeather.location.name);
                TemperatureTextBox.Text = myWeather.current.temp_c.ToString();
                feelsLikeTextBox.Text = myWeather.current.feelslike_c.ToString();
                WindTextBox.Text = myWeather.current.wind_kph.ToString() + " km/h";
                HumidityTextBox.Text = myWeather.current.humidity.ToString();
                CloudinessTextBox.Text = myWeather.current.cloud.ToString() + " %";
                CloudinessProgressBar.Value = myWeather.current.cloud;
                PressureTextBox.Text = myWeather.current.pressure_mb + " hPa";
                DescriptionTextBox.Text = myWeather.current.condition.text;

                string icon = String.Format("http:{0}", myWeather.current.condition.icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            }
            else
            {
                DescriptionTextBox.Text = "Error: Cannot get the forecast. Check your internet connection";
            }
        }
    }
}
