using System;
using WeatherAPI.Services;
using WeatherAPI.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherAPI
{
    public partial class App : Application
    {
        public static RequestManager RequestManager { get; private set; }

        public App()
        {
            InitializeComponent();

            RequestManager = new RequestManager(new RestService());
            MainPage = new NavigationPage(new WeatherPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
