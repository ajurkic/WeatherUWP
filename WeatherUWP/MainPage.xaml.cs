using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.QueryText != "")
            {
                RootObject myWeather = await OpenWeatherMapProxy.GetWeather(args.QueryText);

                if (myWeather.current.condition.code != 400 || 
                    myWeather.current.condition.code != 401 ||
                    myWeather.current.condition.code != 403)
                {
                    TemperatureTextBox.Text = myWeather.current.temp_c.ToString();
                    feelsLikeTextBox.Text = myWeather.current.feelslike_c.ToString();
                    WindTextBox.Text = myWeather.current.wind_kph.ToString() + " km/h";
                    HumidityTextBox.Text = myWeather.current.humidity.ToString();
                    CloudinessTextBox.Text = myWeather.current.cloud.ToString() + " %";
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
}
