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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /*private async void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            RootObject myWeather = await OpenWeatherMapProxy.GetWeather("Split");
            if (myWeather.city != null)
            {
                ResultTextBox.Text = myWeather.city.name + ", " + myWeather.list[0].weather[0].description +
                                        "\nTemp.: " + myWeather.list[0].main.temp + " °C" +
                                        "\nWind speed: " + myWeather.list[0].wind.speed + " m/s" +
                                        "\nPressure: " + myWeather.list[0].main.pressure + " hPa" +
                                        "\nMin. temp: " + myWeather.list[0].main.temp_min + " °C" +
                                        "\nMax. temp: " + myWeather.list[0].main.temp_max + " °C" +
                                        "\nCloudiness: " + myWeather.list[0].clouds.all + " %";

                string icon = String.Format("http://openweathermap.org/img/w/{0}.png", myWeather.list[0].weather[0].icon);
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
            }
            else
            {
                ResultTextBox.Text = "Error: Cannot get the forecast. Check your internet connection";
            }
        }*/

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if(args.QueryText != "")
            {
                RootObject myWeather = await OpenWeatherMapProxy.GetWeather(args.QueryText);
                if (myWeather.city != null)
                {
                    ResultTextBox.Text = myWeather.city.name + ", " + myWeather.list[0].weather[0].description +
                                            "\nTemp.: " + myWeather.list[0].main.temp + " °C" +
                                            "\nWind speed: " + myWeather.list[0].wind.speed + " m/s" +
                                            "\nPressure: " + myWeather.list[0].main.pressure + " hPa" +
                                            "\nMin. temp: " + myWeather.list[0].main.temp_min + " °C" +
                                            "\nMax. temp: " + myWeather.list[0].main.temp_max + " °C" +
                                            "\nCloudiness: " + myWeather.list[0].clouds.all + " %";

                    string icon = String.Format("ms-appx:///Assets/WeatherIcons/{0}.png", myWeather.list[0].weather[0].icon);
                    ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                }
                else
                {
                    ResultTextBox.Text = "Error: Cannot get the forecast. Check your internet connection";
                }
            }
        }
    }
}
