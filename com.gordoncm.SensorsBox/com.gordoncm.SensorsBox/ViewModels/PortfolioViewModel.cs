﻿using com.gordoncm.SensorsBox.Database; 
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
using com.gordoncm.SensorsBox.Models;

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
        private string _portfolioMsg = "";
        private User user;
        private int _rowHeight; 

        public string PortfolioMsg
        {
            get { return _portfolioMsg; } 
            set
            {
                _portfolioMsg = value;
                OnPropertyChanged("PortfolioMsg");
            }
        }

        public int RowHeight
        {
            get { return _rowHeight; }
            set
            {
                _rowHeight = value;
                OnPropertyChanged("RowHeight");
            }
        }

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

            try
            {
                user = _db.GetUserAsync().Result;
            }
            catch (Exception)
            {
            }

            if (user == null)
            {
                user = Utils.createFakeUser(); 
            }

            RowHeight = Utils.GetRowHeight(user.FontSize);

            LBLMsg = "Press refresh to update portfolio, and change font sizes and colors from settings";
            PortfolioMsg = user.PreferedName + "'s Portfolio"; 

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
                PortfolioMsg = _db.GetUserAsync().Result.PreferedName + "'s Portfolio";


                Items.Clear();

                _connection.Table<Models.Portfolio>().DeleteAsync();

                var user2 = _db.GetUserAsync().Result;

                if (user2 != null)
                {
                    user = user2;
                }

                RowHeight = Utils.GetRowHeight(user.FontSize);

                UpdatePortfolio(_db.GetUserAsync().Result.ETHWalletAddress);

                ListViewIsVisible = true;

            }
            catch
            {
                LBLMsg = "Error occured while refreshing"; 
            }

        }

        private void UpdateToObs(List<Models.Portfolio> result)
        {
            var tempList = new ObservableCollection<Models.Portfolio>();

            foreach (Models.Portfolio item in result)
            {
                item.PrimaryColor = user.PrimaryColor;
                item.SecondaryColor = user.SecondaryColor;
                item.FontSize = Utils.GetFontSize(user.FontSize); 

                Items.Add(item);
            }

            ListViewIsVisible = true; 
        }

        public async void UpdatePortfolio(string walletAddress)
        {
            try
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
                                LBLMsg = "Error occured while refreshing"; 
                            }


                            string tokenName = resultResponse[0].tokenName;

                            await _db.AddToPortfolio(tokenName, h);

                            var portfolio = new Models.Portfolio();
                            portfolio.CoinName = tokenName;
                            portfolio.CoinAmount = h;
                            portfolio.PrimaryColor = user.PrimaryColor;
                            portfolio.SecondaryColor = user.SecondaryColor;
                            portfolio.FontSize = Utils.GetFontSize(user.FontSize); 

                            Items.Add(portfolio);

                            tokenCounter++;
                        }
                        catch
                        { 
                        }
                    }
                }
            }
            catch
            {
                LBLMsg = "Error refreshing"; 
            }

            LBLMsg = "Done refreshing"; 
        }
    }
}
