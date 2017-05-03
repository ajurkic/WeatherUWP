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

        private async void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            /*if(nestoReason == AutoSuggestionBoxTextChangeReason.UserInput) //doradit ovo s args
            {
                
                ako se promjenija input
                    pogledat jel postoji grad unutar city.list.json filea

                KAKO BRZO PROVJERIT SVE GRADOVE A DA NE ZAUZMEN PREVIŠE MEMORIJE???

                
            }*/
        }

        private async void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if(args.QueryText != "")
            {
                RootObject myWeather = await OpenWeatherMapProxy.GetWeather(args.QueryText);

                if (myWeather.city != null)
                {
                    TemperatureTextBox.Text = myWeather.list[0].main.temp.ToString();
                    MinTextBox.Text = myWeather.list[0].main.temp_min.ToString();
                    MaxTextBox.Text = myWeather.list[0].main.temp_max.ToString();
                    WindTextBox.Text = myWeather.list[0].wind.speed + " m/s";
                    HumidityTextBox.Text = myWeather.list[0].main.humidity.ToString();
                    CloudinessTextBox.Text = myWeather.list[0].clouds.all + " %";
                    PressureTextBox.Text = myWeather.list[0].main.pressure + " hPa";
                    DescriptionTextBox.Text = myWeather.list[0].weather[0].description;

                    string icon = String.Format("ms-appx:///Assets/WeatherIcons/{0}.png", myWeather.list[0].weather[0].icon);
                    ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                }
                else
                {
                    TemperatureTextBox.Text = "Error: Cannot get the forecast. Check your internet connection";
                }
            }
        }
    }
}
