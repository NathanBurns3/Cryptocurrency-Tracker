﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using com.gordoncm.SensorsBox.Models; 

namespace com.gordoncm.SensorsBox.ViewModels
{
    public  class CoinDetailViewModel : BaseViewModel
    {
        private decimal _price {  get; set; }
        private string _name {  get; set; }
        private string _cmcRank {  get; set; }
        private string _circulatingSupply { get; set; }
        private string _totalSupply { get; set; }
        private string _maxSupply { get; set; }

        public CoinDetailViewModel(Coin coin) 
        {
            Name = coin.Name;
            CMCRank = coin.CMCRank;
            if (coin.CirculatingSupply == null || coin.CirculatingSupply == string.Empty)
            {
                CirculatingSupply = "N/A";
            }
            else
            {
                CirculatingSupply = coin.CirculatingSupply;
            }
            if (coin.TotalSupply == null || coin.TotalSupply == string.Empty)
            {
                TotalSupply = "N/A";
            }
            else
            {
                TotalSupply = coin.TotalSupply;
            }
            if (coin.MaxSupply == null || coin.MaxSupply == string.Empty)
            {
                MaxSupply = "N/A";
            }
            else
            {
                MaxSupply = coin.MaxSupply;
            }

            decimal h = Decimal.Parse(
                 coin.Price,
                 NumberStyles.Any,
                 CultureInfo.InvariantCulture);
                 coin.SENotation = h;

            Price = h;
        }

        public decimal Price
        {
            get { return _price; } 
            set
            {
                _price = value;
                OnPropertyChanged("Price"); 
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name"); 
            }
        }

        public string CMCRank
        {
            get { return _cmcRank; }
            set
            {
                _cmcRank = value;
                OnPropertyChanged("CMCRank"); 
            }
        }

        public string CirculatingSupply
        {
            get { return _circulatingSupply; }
            set
            {
                _circulatingSupply = value;
                OnPropertyChanged("CirculatingSupply"); 
            }
        }

        public string TotalSupply
        {
            get { return _totalSupply; }
            set
            {
                _totalSupply = value;
                OnPropertyChanged("TotalSupply"); 
            }
        }

        public string MaxSupply
        {
            get { return _maxSupply; }
            set
            {
                _maxSupply = value;
                OnPropertyChanged("MaxSupply"); 
            }
        }
    }
}
