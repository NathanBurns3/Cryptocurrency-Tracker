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

        public void DeleteCoins()
        {
            string deleteQuery = "DELETE FROM coins";
            var deleteCmd = _connection.CreateCommand(deleteQuery);

            deleteCmd.ExecuteNonQuery(); 
        }

        public void UpdateUserAddress(int UserId, string Address)
        {
            string updateAddress = "UPDATE Users SET WalletAddress = " + '"'+Address+'"';

            var updateCmd = _connection.CreateCommand(updateAddress); 
            updateCmd.ExecuteNonQuery();
        }
    }
}
