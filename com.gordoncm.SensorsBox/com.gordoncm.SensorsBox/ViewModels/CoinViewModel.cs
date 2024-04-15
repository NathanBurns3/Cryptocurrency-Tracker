using com.gordoncm.SensorsBox.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using com.gordoncm.SensorsBox.Database;
using System.Windows.Input;
using Xamarin.Forms;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class CoinViewModel : BaseViewModel
    {
        public ObservableCollection<Coin> Items { get; set; }

        public ICommand LoadMoreCmd {  get; set; }
        public ICommand RefreshCmd {  get; set; }

        private CryptoDB _db {  get; set; }
        private SQLiteAsyncConnection _connection {  get; set; }
        private bool _listViewIsVisible = false;

        public bool ListViewIsVisible
        {
            get { return _listViewIsVisible; }
            set
            {
                _listViewIsVisible = value;
                OnPropertyChanged();
            }
        }

        public CoinViewModel() 
        {
            _db = new CryptoDB();
            Items = new ObservableCollection<Coin>();

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Models.Coin>();

            RefreshCmd = new Command(Refresh); 
            LoadMoreCmd = new Command(LoadMore);
        }

        private void Refresh()
        {
            Items.Clear();

            _connection.DeleteAllAsync<Models.Coin>(); 

            try
            {
                var result = _connection.Table<Models.Coin>().ToListAsync().Result;

                if (result.Count != 0)
                {
                    UpdateToObs();
                }
                else
                {
                    CreateIfNew();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message; 
            }

        }

        private void LoadMore()
        {
        } 

        private void UpdateToObs()
        {
            var coins = _db.GetCoins(0).Result;
            foreach (var coin in coins)
            {
                Items.Add(coin); 
            }

            ListViewIsVisible = true;
        }



        private async void CreateIfNew()
        { 

            string result = await ApiCaller.getListings();
            dynamic results = JsonConvert.DeserializeObject<dynamic>(result);
            dynamic data = results.data; 

            try
            {
                foreach (var coin in data)
                {
                    string name = coin.name;
                    string cmcRank = coin.cmc_rank;
                    string circulatingSupply = coin.circulating_supply;
                    string totalSupply = coin.total_supply;
                    string maxSupply = coin.max_supply;

                    string price = "";

                    dynamic quote = coin.quote;

                    if (quote != null)
                    {
                        price = quote.USD.price;
                    } 

                    await _db.AddCoin(name, cmcRank, circulatingSupply, totalSupply, maxSupply, price);
                };

                UpdateToObs(); 
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
