using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherAPI.Model;
using Xamarin.Forms.Maps;

namespace WeatherAPI.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public Root Root { get; set; }
        public WeatherPage()
        {
            InitializeComponent();
            Root = new Root();   
            BindingContext = Root;
        }

        private async void SBSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            var city = SBSearch.Text;
            Root = await App.RequestManager.GetWeather(city);

            if (Root.wind.speed > 0)
            {
                float tempWindDeg = Root.wind.deg / 45;

                switch (Math.Round(tempWindDeg))
                {
                    case 0:
                        Root.wind.direction = "→";
                        break;
                    case 1:
                        Root.wind.direction = "↗";
                        break;
                    case 2:
                        Root.wind.direction = "↑";
                        break;
                    case 3:
                        Root.wind.direction = "↖";
                        break;
                    case 4:
                        Root.wind.direction = "←";
                        break;
                    case 5:
                        Root.wind.direction = "↙";
                        break;
                    case 6:
                        Root.wind.direction = "↓";
                        break;
                    case 7:
                        Root.wind.direction = "↘";
                        break;
                    case 8:
                        Root.wind.direction = "→";
                        break;
                }
            }
            else
                Root.wind.direction = "*";

            WeatherImg.Source = new UriImageSource { Uri = new Uri($"http://openweathermap.org/img/w/{Root.weather[0].icon}.png") };

            MoveMap();
            BindingContext = Root;
        }
        private void MoveMap()
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Root.coord.lat, Root.coord.lon), Distance.FromKilometers(50)));

            map.ItemsSource = new List<Root>
            {
                new Root
                {
                    name = Root.name,
                    position = new Position(Root.coord.lat, Root.coord.lon),
                    sys = new Sys
                    {
                        country = Root.sys.country
                    }
                }
            };
            BindingContext = Root;
        }
    }
}