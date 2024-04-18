using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.gordoncm.SensorsBox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        // https://stackoverflow.com/questions/71357377/search-filter-in-xamarin-listview

        public List<string> coins = new List<string>();
        private CryptoDB _db; 

        public Search()
        {
            InitializeComponent();

             _db = new CryptoDB();
            var coinsDb = _db.GetAllCoins().Result; 

            foreach (var coin in coinsDb)
            {
                coins.Add(coin.Name);
            }

            CountrySearchList.ItemsSource = coins;
        }

        public void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var countriesSearched = coins.Where(c => c.Contains(CountriesSearchBar.Text));
            CountrySearchList.ItemsSource = countriesSearched;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string coinName = (string)e.SelectedItem;
            var coin = _db.GetCoinByName(coinName).Result;


            await Navigation.PushAsync(new CoinDetail(coin)); 
        }
    }
}