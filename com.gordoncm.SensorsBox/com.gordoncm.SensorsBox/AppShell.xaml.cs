using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace com.gordoncm.SensorsBox
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CoinList), typeof(CoinList));
            Routing.RegisterRoute(nameof(Portfolio), typeof(Portfolio));
            Routing.RegisterRoute(nameof(MyFavs), typeof(MyFavs));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(CoinDetail), typeof(CoinDetail));
        }
    }
}
