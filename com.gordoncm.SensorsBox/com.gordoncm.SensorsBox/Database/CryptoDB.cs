using com.gordoncm.SensorsBox.Models;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace com.gordoncm.SensorsBox.Database
{
    public  class CryptoDB
    {

        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<CryptoDB> Instance = new AsyncLazy<CryptoDB>(async () =>
        {
            var instance = new CryptoDB();

            CreateTableResult coinResult = await Database.CreateTableAsync<Coin>();
            CreateTableResult userResult = await Database.CreateTableAsync<User>();
            CreateTableResult favoriteResult = await Database.CreateTableAsync<Favorite>();

            return instance;
        });

        public CryptoDB()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            CreateDefaultUserIfNoneExists().Wait();
        }

        private async Task CreateDefaultUserIfNoneExists()
        {
            var userCount = await Database.Table<User>().CountAsync();

            if (userCount == 0)
            {
                var defaultUser = new User
                {
                    UserId = 1,
                    UserName = "DefaultUser",
                    PreferedName = "Default User",
                    WalletAddress = "0x",
                    PrimaryColor = "Blue",
                    SecondaryColor = "Orange",
                    FontSize = "Medium",
                    Currency = "USD",
                };

                await Database.InsertAsync(defaultUser);
            }
        }

        public Task<User> GetUserAsync()
        {
            return Database.Table<User>().FirstOrDefaultAsync();
        }

        public Task<List<Favorite>> GetFavorites()
        {
            return Database.Table<Favorite>().ToListAsync();
        }

        public Task<Coin> GetCoin(int coinId)
        {
            return Database.Table<Coin>().Where(i => i.CoinId == coinId).FirstOrDefaultAsync();
        }

        public Task<List<Coin>> GetCoins(int maxId, int pageId)
        {
            return Database.Table<Coin>().Where(i => i.CoinId <= maxId && i.CoinId >= pageId).ToListAsync();
        }

        public Task<int> DeleteFavorite(Favorite favorite)
        {
            return Database.DeleteAsync(favorite);
        }

        public async Task<int> DeleteCoins()
        {
            var coins = await Database.Table<Coin>().ToListAsync();
            int deleteCount = 0;
            foreach (var coin in coins)
            {
                deleteCount += await Database.DeleteAsync(coin);
            }
            return deleteCount;
        }
    }
}
