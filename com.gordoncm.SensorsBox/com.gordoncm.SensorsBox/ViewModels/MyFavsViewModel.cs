using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class MyFavsViewModel : BaseViewModel
    {

        public ObservableCollection<Coin> MyFavs { get; set; }
        
        private CryptoDB _db;
        private SQLiteAsyncConnection _connection;
        private bool _listViewIsVisible = false;
        private string _favoritesMsg = "";
        private User user;
        private int _rowHeight;
        public ICommand RefreshCmd { get; set; }
        public ICommand ClearCmd { get; set; }
        public string FavoritesMsg
        {
            get { return _favoritesMsg; }
            set
            {
                _favoritesMsg = value;
                OnPropertyChanged("FavoritesMsg");
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

        public bool ListViewIsVisible
        {
            get { return _listViewIsVisible; }
            set
            {
                _listViewIsVisible = value;
                OnPropertyChanged();
            }
        }
        public MyFavsViewModel()
        {
            _db = new CryptoDB();
            MyFavs = new ObservableCollection<Coin>();

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

            FavoritesMsg = "Refresh the page to view favorites";
            RowHeight = Utils.GetRowHeight(user.FontSize);

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Favorite>();

            ClearCmd = new Command(ClearFavs);
            RefreshCmd = new Command(PopulateFavs);

        }

        private async void PopulateFavs()
        {
            FavoritesMsg = "Loading favorites...";
            MyFavs.Clear();
            var favs = await _db.GetFavorites();
            foreach(var fav in favs)
            {
                if (!String.IsNullOrEmpty(fav.Name))
                {
                    var coin = await _db.GetCoinByName(fav.Name);

                    if (!MyFavs.Contains(coin))
                    {
                        coin.PrimaryColor = user.PrimaryColor;
                        coin.SecondaryColor = user.SecondaryColor;
                        MyFavs.Add(coin);
                    }
                }
                else continue;
                
            }

            RowHeight = Utils.GetRowHeight(user.FontSize);
            
            ListViewIsVisible = true;
            FavoritesMsg = "Favorites refreshed";
        }

        private async void ClearFavs()
        {
            var favs = await _db.GetFavorites();
            foreach (var fav in favs)
            {
                await _db.DeleteFavorite(fav);
            }
            MyFavs.Clear();
            ListViewIsVisible = false;
            FavoritesMsg = "Favorites cleared";
        }
    }
}