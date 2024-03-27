using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
