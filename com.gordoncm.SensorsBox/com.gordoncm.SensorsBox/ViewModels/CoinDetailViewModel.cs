using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.Database;
using System.Windows.Input;
using Xamarin.Forms;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public  class CoinDetailViewModel : BaseViewModel
    {
        private CryptoDB _db;
        private decimal _price {  get; set; }
        private string _name {  get; set; }
        private string _cmcRank {  get; set; }
        private string _circulatingSupply { get; set; }
        private string _totalSupply { get; set; }
        private string _maxSupply { get; set; }
        private string _primaryColor { get; set; }
        private string _secondaryColor { get; set; }
        private string _buttonText { get; set; }
        public ICommand FavoriteButtonCmd { get; set; }

        public CoinDetailViewModel(Coin coin) 
        {
            _db = new CryptoDB();
            
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

            User user = null;

            try
            {
                user = new CryptoDB().GetUserAsync().Result; 
            }
            catch (Exception)
            {
            }

            if (user == null)
            {
                user = Utils.createFakeUser(); 
            }

            if (user.Currency != "USD")
            {
                if (h > 0.01M)
                {
                    if (user.Currency == "Pesos")
                    {
                        double price = Utils.convertToPesos(coin.Price);

                        Price = Utils.convert(price.ToString());
                    }
                    else if (user.Currency == "CAD")
                    {
                        double price = Utils.convertToCad(coin.Price);

                        Price = Utils.convert(price.ToString());
                    }
                }
            }

            if (IsFavorite(Name))
            {
                ButtonText = "Remove from Favorites";
                FavoriteButtonCmd = new Command(RemoveFromFavorites);
            }
            else
            {
                ButtonText = "Add to Favorites";
                FavoriteButtonCmd = new Command(AddToFavorites);
            }

            PrimaryColor = user.PrimaryColor;
            SecondaryColor = user.SecondaryColor;
        }


        public string PrimaryColor
        {
            get { return _primaryColor; } 
            set { _primaryColor = value;
                OnPropertyChanged("PrimaryColor");
            }
        }

        public string SecondaryColor
        {
            get { return _secondaryColor; } 
            set
            {
                _secondaryColor = value;
                OnPropertyChanged("SecondaryColor"); 
            }
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
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged("ButtonText");
            }
        }


        private async void AddToFavorites()
        {
            await _db.AddCoinToFavorites(Name);
            NavigateBack();
        }
        private async void RemoveFromFavorites()
        {
            var fav = await _db.GetFavoriteByName(Name);
            await _db.DeleteFavorite(fav);
            NavigateBack();
        }
        private async void NavigateBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        
        private bool IsFavorite(string name)
        {
            var fav = _db.GetFavoriteByName(name).Result;
            if (fav == null)
            {
                return false;
            }
            return true;
        }
    }
}
