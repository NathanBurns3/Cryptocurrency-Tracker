using com.gordoncm.SensorsBox.Database;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestSharp;
using SQLite;

namespace com.gordoncm.SensorsBox
{
    public partial class App : Application
    {
        public App()
        {
            var provider = new Dependencies().Load().GetServiceProvider(); 
            InitializeComponent();

            MainPage = new AppShell();

            var connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            connection.CreateTableAsync<Models.User>();

            var cryptoDb = new CryptoDB(); 
        }



        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
        { 
        }
    }
}
