using com.gordoncm.SensorsBox.Database; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq; 
using System.Windows.Input;
using System.Runtime;
using SQLite;
using Xamarin.Forms;
using System.Globalization;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class PortfolioViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Portfolio> Items { get; set; }
        public ICommand RefreshCmd {  get; set; }


        private CryptoDB _db; 
        private SQLiteAsyncConnection _connection;
        private bool _listViewIsVisible = false; 
        private string _lblMsg = "";

        public string LBLMsg
        {
            get { return _lblMsg; }
            set
            {
                _lblMsg = value;
                OnPropertyChanged("LBLMsg");
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


        public PortfolioViewModel()
        {
            _db = new CryptoDB();
            Items = new ObservableCollection<Models.Portfolio>();
            RefreshCmd = new Command(Refresh); 

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Models.Portfolio>();

            LBLMsg = "Press refresh to update portfolio";

            try
            {
                UpdateToObs(_connection.Table<Models.Portfolio>().ToListAsync().Result);
            }
            catch
            {
            }
        } 

        private void Refresh()
        {
            try
            {
                LBLMsg = "Refreshing"; 

                Items.Clear();

                _connection.Table<Models.Portfolio>().DeleteAsync(); 

                UpdatePortfolio("0x3f5ce5fbfe3e9af3971dd833d26ba9b5c936f0be");


                ListViewIsVisible = true;
            }
            catch
            {
            }

        }

        private void UpdateToObs(List<Models.Portfolio> result)
        {
            var tempList = new ObservableCollection<Models.Portfolio>();

            foreach (Models.Portfolio item in result)
            {
                Items.Add(item);
            }

            ListViewIsVisible = true; 
        }

        public async void UpdatePortfolio(string walletAddress)
        {
            var apiCaller = new ApiCaller();
            string response = await apiCaller.GetETHPortfolio(walletAddress);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response);
            dynamic result = results.result;
            dynamic tokenBalances = result.tokenBalances;

            var tokenCounter = 0;

            foreach (var token in tokenBalances)
            {
                if (tokenCounter <= 30)
                {
                    string contractAddress = token.contractAddress;
                    string tokenBalance = token.tokenBalance;

                    // http://94.158.244.85:3000/?id=0x000000000000000000000000000000000000000000108b2a2c28029094000000
                    try
                    {
                        string tokenNameResult = await apiCaller.GetTokenFromTx(contractAddress);
                        dynamic tokenNameConvert = JsonConvert.DeserializeObject<dynamic>(tokenNameResult);
                        dynamic resultResponse = tokenNameConvert.result;

                        decimal h = 0;

                        try
                        {
                            string balanceResult = await apiCaller.GetTokenBalance(tokenBalance);

                            string finalBalanceResult = balanceResult.Replace("\"", "").Replace("\\", "");

                            h = Decimal.Parse(
                                    finalBalanceResult,
                                    NumberStyles.Any,
                                     CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                        }


                        string tokenName = resultResponse[0].tokenName; 

                        await _db.AddToPortfolio(tokenName, h);

                        var portfolio = new Models.Portfolio();
                        portfolio.CoinName = tokenName;
                        portfolio.CoinAmount = h;

                        Items.Add(portfolio);

                        tokenCounter++;  
                    }
                    catch
                    { 
                    }
                }
            }

            LBLMsg = "Done refreshing"; 
        }
    }
}
