using com.gordoncm.SensorsBox.Database;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestSharp; 

namespace com.gordoncm.SensorsBox
{
    public partial class App : Application
    {
        public App()
        {
            var provider = new Dependencies().Load().GetServiceProvider(); 
            InitializeComponent();

            MainPage = new AppShell();
        }



        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        {

        }
    }
}
