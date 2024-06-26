﻿using com.gordoncm.SensorsBox.Models;
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
            CreateTableResult portfolioResult = await Database.CreateTableAsync<Models.Portfolio>(); 

            return instance;
        });

        public void CreateUser()
        {
        }

        public CryptoDB()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags); CreateDefaultUserIfNoneExists();
            CreateDefaultUserIfNoneExists(); 
        }

        public async Task AddCoin(string coinName, string cmcRank, string circulatingSupply, 
            string totalSupply, string maxSupply, string Price)
        {
            var _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Models.Coin>();

            var coin = new Coin()
            {
                Name = coinName, 
                CMCRank = cmcRank,
                CirculatingSupply = circulatingSupply,
                TotalSupply = totalSupply,
                MaxSupply = maxSupply,
                Price = Price,  
            };

            await _connection.InsertAsync(coin);
        }

        public async Task<List<Models.Coin>> GetCoins()
        {
            var _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Models.Coin>();

            var result = _connection.Table<Models.Coin>().ToListAsync().Result;
            var coins = result.OrderBy(p => p.CMCRank).ToList();

            return coins;
        }

        public async Task AddToPortfolio(string coinName, decimal coinAmount)
        {
            var _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Models.Portfolio>();

            var portfolio = new Models.Portfolio
            {
                CoinName = coinName,
                CoinAmount = coinAmount,   
            };

            await _connection.InsertAsync(portfolio);
        }

        public async Task AddCoinToFavorites(string name)
        {
            var _connection = new SQLiteAsyncConnection(Constants.DatabasePath);
            _connection.CreateTableAsync<Favorite>();

            var favorite = new Favorite
            {
                Name = name
            };
            await _connection.InsertAsync(favorite);
        }
        

        public async Task CreateDefaultUserIfNoneExists()
        {
            var userCount = await Database.Table<User>().CountAsync();

            if (userCount == 0)
            {
                var defaultUser = new User
                {
                    UserId = 1,
                    UserName = "DefaultUser",
                    PreferedName = "Default User",
                    ETHWalletAddress = "0x3f5ce5fbfe3e9af3971dd833d26ba9b5c936f0be", 
                    PrimaryColor = "Blue",
                    SecondaryColor = "Orange",
                    FontSize = "Small",
                    Currency = "USD",
                };

                await Database.InsertAsync(defaultUser);
            }
        }

        public Task<int> UpdateUserAsync(User user)
        {
            return Database.UpdateAsync(user);
        }

        public Task<User> GetUserAsync()
        {
            return Database.Table<User>().FirstOrDefaultAsync();
        }

        public Task<List<Models.Portfolio>> GetPortfolios()
        {
           return Database.Table<Models.Portfolio>().ToListAsync();
        }

        public Task<List<Favorite>> GetFavorites()
        {
            return Database.Table<Favorite>().ToListAsync();
        }

        public Task<Coin> GetCoin(int coinId)
        {
            return Database.Table<Coin>().Where(i => i.CoinId == coinId).FirstOrDefaultAsync();
        }

        public Task<List<Coin>> GetCoins(int skip)
        {
            return Database.Table<Coin>().Skip(skip).Take(50).ToListAsync();
        }

        public Task<List<Coin>> GetAllCoins()
        {
            return Database.Table<Coin>().ToListAsync(); 
        }

        public Task<Coin> GetCoinByName(string name)
        {
            return Database.Table<Coin>().Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public Task<Favorite> GetFavoriteByName(string name)
        {
            return Database.Table<Favorite>().Where(f => f.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> DeleteFavorite(Favorite favorite)
        {
            return Database.DeleteAsync(favorite);
        }

        public async Task<int> DeletePortfolio()
        {
            var coins = await Database.Table<Models.Portfolio>().ToListAsync(); 

            foreach (var coin in coins)
            {
                await Database.DeleteAsync(coin); 
            }

            return 0;
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
