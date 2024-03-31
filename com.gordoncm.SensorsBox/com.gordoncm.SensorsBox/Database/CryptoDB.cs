using com.gordoncm.SensorsBox.Models;
using SQLite; 
using System.Collections.ObjectModel;
using System.Linq; 

namespace com.gordoncm.SensorsBox.Database
{
    public  class CryptoDB
    {
        private readonly SQLiteConnection _connection;

        public CryptoDB(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);

            _connection.CreateTable<Coin>();
            _connection.CreateTable<User>();
            _connection.CreateTable<Favorite>();
        } 

        public User getUser()
        {
            string userQuery = "SELECT * FROM Users";

            var cmd = _connection.CreateCommand(userQuery);
            User user = cmd.ExecuteQuery<User>().First();

            return user; 
        }

        public ObservableCollection<Favorite> GetFavorites()
        {
            string getFavs = "SELECT * FROM Favorites";

            var cmd = _connection.CreateCommand(getFavs);
            var list = cmd.ExecuteQuery<Favorite>().ToList(); 

            var obs = new ObservableCollection<Favorite>();

            foreach (var item in list)
            {
                obs.Add(item);
            }

            return obs; 
        }

        public Coin GetCoin(int coinId)
        {
            string singleCoin = "SELECT * FROM Coins WHERE CoinId = " + coinId;

            var cmd = _connection.CreateCommand(singleCoin);
            var coin = cmd.ExecuteQuery<Coin>().First();

            return coin; 
        }

        public ObservableCollection<Coin> GetCoins(int maxId, int pageId)
        {
            string getCoinsQuery = "SELECT * FROM Coins WHERE CoinId <= " + maxId + " AND CoinId >=" +pageId; 
            
            var coins = _connection.CreateCommand(getCoinsQuery);
            var list = coins.ExecuteQuery<Coin>().ToList();
            var obs = new ObservableCollection<Coin>();

            foreach (var coin in list)
            {
                obs.Add(coin);
            }
            
            return obs; 
        }

        public void DeleteFavorite(int id)
        {
            string deleteQuery = "DELETE FROM Favorites WHERE Id = " + id; 
            var deleteCmd = _connection.CreateCommand(deleteQuery);

            deleteCmd.ExecuteNonQuery(); 
        }

        public void DeleteCoins()
        {
            string deleteQuery = "DELETE FROM Coins";
            var deleteCmd = _connection.CreateCommand(deleteQuery);

            deleteCmd.ExecuteNonQuery(); 
        }

        public void UpdateUserAddress(int UserId, string Address)
        {
            string updateAddress = "UPDATE Users SET WalletAddress = " + '"' + Address + '"' + " WHERE UserId = " + UserId; 

            var updateCmd = _connection.CreateCommand(updateAddress); 
            updateCmd.ExecuteNonQuery();
        }
    }
}
