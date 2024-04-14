using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.gordoncm.SensorsBox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinList : ContentPage
    {
        CoinViewModel coinViewModel;
        public CoinList()
        { 
            InitializeComponent();
            coinViewModel = new CoinViewModel();
            BindingContext = coinViewModel; 
        }

        protected override async void OnAppearing()
        {
            coinViewModel.Items = await GetCoins();
        }

        public async Task<List<Coin>> GetCoins()
        {
            var options = new RestClientOptions("https://rest.coinapi.io/v1/")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("https://rest.coinapi.io/v1/exchangerate/USD", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-CoinAPI-Key", "F11A11C0-D39B-5DB9-CA49-AAF13A7CF742");
            RestResponse response = await client.ExecuteAsync(request);

            var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
            List<Coin> coins = JsonConvert.DeserializeObject<List<Coin>>(jsonResponse["rates"].ToString());
            List<Coin> tenCoins = new List<Coin>();

            // too many coins to load, so just returning the top 10
            for (int i = 0; i < 10; i++)
            {
                tenCoins.Add(coins[i]);
            }

            return tenCoins;
        }
    }
}