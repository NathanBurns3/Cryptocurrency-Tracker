using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Windows.Input;
using System.Runtime;
using SQLite;
using Xamarin.Forms;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class PortfolioViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Portfolio> Items { get; set; }
        public ICommand RefreshCmd {  get; set; }


        private CryptoDB _db; 
        private SQLiteAsyncConnection _connection;
        private bool _listViewIsVisible = false;
        private bool _lblIsVisible = true;
        private string _lblMsg = "Press refresh to update portfolio"; 

        public string LBLMsg
        {
            get { return _lblMsg; } set { _lblMsg = value; }
        }

        public bool LBLIsVisible
        {
            get { return _lblIsVisible; }
            set { _lblIsVisible = value; }
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
        } 

        private void Refresh()
        {
            try
            {
                Items.Clear(); 

                var result = _connection.Table<Models.Portfolio>().ToListAsync().Result;

                if (result.Count() != 0)
                {
                    UpdateToObs(result);
                }
                else
                {
                    UpdatePortfolio("0x3f5ce5fbfe3e9af3971dd833d26ba9b5c936f0be");
                }

                _lblIsVisible = false;
                ListViewIsVisible = true; 
            }
            catch (Exception ex)
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
        }

        public async void UpdatePortfolio(string walletAddress)
        {
            //
            // 

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

                    try
                    {
                        string tokenNameResult = await apiCaller.GetTokenFromTx(contractAddress);
                        dynamic tokenNameConvert = JsonConvert.DeserializeObject<dynamic>(tokenNameResult);
                        dynamic resultResponse = tokenNameConvert.result;

                        string tokenName = resultResponse[0].tokenName; 

                        await _db.AddToPortfolio(tokenName, 10);

                        var portfolio = new Models.Portfolio();
                        portfolio.CoinName = tokenName;
                        portfolio.CoinAmount = 10000;

                        Items.Add(portfolio);

                        tokenCounter++;  
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        _lblMsg = message; 
                    }
                }
            }
        }
    }
}
