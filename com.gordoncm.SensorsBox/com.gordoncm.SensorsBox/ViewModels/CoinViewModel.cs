using com.gordoncm.SensorsBox.Models;
using SQLite;
using System; 
using System.Collections.ObjectModel;  
using Newtonsoft.Json;
using com.gordoncm.SensorsBox.Database;
using System.Windows.Input;
using Xamarin.Forms; 

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class CoinViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public ObservableCollection<Coin> Items { get; set; }

        public ICommand LoadMoreCmd {  get; set; }
        public ICommand RefreshCmd {  get; set; }
        public ICommand SearchCmd {  get; set; }

        private CryptoDB _db {  get; set; }
        private SQLiteAsyncConnection _connection {  get; set; }
        private bool _listViewIsVisible = false;
        private string _lblRefresh;
        private int _rowHeight;
        private string _test;
        private User user; 

        public int RowHeight
        {
            get { return _rowHeight; }
            set
            {
                _rowHeight = value;
                OnPropertyChanged("RowHeight"); 
            }
        }

        public bool ListViewIsVisible
        {
            get { return _listViewIsVisible; }
            set
            {
                _listViewIsVisible = value;
                OnPropertyChanged();
            }
        }

        public string LBLRefresh
        {
            get { return _lblRefresh; }
            set
            {
                _lblRefresh = value;
                OnPropertyChanged("LBLRefresh");
            }
        }

        public CoinViewModel(INavigation navigation) 
        {
            this.Navigation = navigation;

            Items = new ObservableCollection<Coin>();

            _db = new CryptoDB();

            try
            {
                user = _db.GetUserAsync().Result;
            }
            catch (SQLiteException)
            {
            }

            if (user == null)
            {
                user = Utils.createFakeUser(); 
            }

            RowHeight = Utils.GetRowHeight(user.FontSize); 

            LBLRefresh = "Press refresh to get new coin prices, and update fonts and colors from settings"; 

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Models.Coin>();

            RefreshCmd = new Command(Refresh); 
            LoadMoreCmd = new Command(LoadMore);
            SearchCmd = new Command(Search);

            try
            {
                UpdateToObs();
            }
            catch
            {
                LBLRefresh = "Error refreshing, try again"; 
            }
        }

        private async void Search()
        {
            await Navigation.PushAsync(new Search()); 
        }

        private void Refresh()
        {
            LBLRefresh = "Refreshing..."; 

            Items.Clear();

            try
            {
                var user2 = _db.GetUserAsync().Result;

                if (user2 != null)
                {
                    user = user2;
                }

                RowHeight = Utils.GetRowHeight(user.FontSize); 
            }
            catch
            {

            }

            _connection.DeleteAllAsync<Models.Coin>(); 

            try
            {
                CreateIfNew();

            }
            catch 
            {
                LBLRefresh = "Error refreshing, try again"; 
            }

        }

        private void LoadMore()
        {
            if (Items.Count > 0)
            {
                var skipCount = Items.Count;

                var coins = _db.GetCoins(skipCount).Result;

                foreach (var coin in coins)
                {
                    if (coin.Price != "")
                    {
                        if (Utils.convert(coin.Price) < 0.01M)
                        {
                            coin.SENotation = Utils.convert(coin.Price);
                        }
                        else
                        {
                            if (user.Currency != "USD")
                            {
                                if (user.Currency == "CAD")
                                {
                                    double price = Utils.convertToCad(coin.Price);
                                    coin.SENotation = Utils.convert(price.ToString()); 
                                }
                                else if (user.Currency == "Pesos")
                                {
                                    double price = Utils.convertToPesos(coin.Price);
                                    coin.SENotation = Utils.convert(price.ToString()); 
                                }
                            }
                            else
                            {
                                coin.SENotation = Utils.convert(coin.Price);
                            }
                        }

                        coin.PrimaryColor = user.PrimaryColor; 
                        coin.SecondaryColor = user.SecondaryColor;

                        Items.Add(coin);
                    }
                }
            }
        } 

        private void UpdateToObs()
        {
            var fontSize = Utils.GetFontSize(user.FontSize); 

            var coins = _db.GetCoins(0).Result;

            foreach (var coin in coins)
            {
                if (coin.Price != "")
                { 
                    coin.FontSize = fontSize;
                    coin.PrimaryColor = user.PrimaryColor;
                    coin.SecondaryColor = user.SecondaryColor;

                    if (Utils.convert(coin.Price) < 0.01M)
                    {
                        coin.SENotation = Utils.convert(coin.Price);
                    }
                    else
                    {
                        if (user.Currency != "USD")
                        {
                            if (user.Currency == "CAD")
                            {
                                double price = Utils.convertToCad(coin.Price);
                                coin.SENotation = Utils.convert(price.ToString());
                            }
                            else if (user.Currency == "Pesos")
                            {
                                double price = Utils.convertToPesos(coin.Price);
                                coin.SENotation = Utils.convert(price.ToString());
                            }
                        }
                        else
                        {
                            coin.SENotation = Utils.convert(coin.Price);
                        }
                    }

                    Items.Add(coin); 
                }
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

                LBLRefresh = "Done Refreshing"; 
            }
            catch (Exception ex) 
            {
                LBLRefresh = ex.Message; 
            }
        }
    }
}
