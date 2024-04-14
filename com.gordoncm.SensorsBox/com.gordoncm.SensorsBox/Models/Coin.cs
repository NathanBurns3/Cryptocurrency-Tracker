using Newtonsoft.Json;
using SQLite;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Coins")]
    public class Coin
    {
        [PrimaryKey][AutoIncrement]
        public int CoinId {  get; set; }

        [JsonProperty("asset_id_quote")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public double Price { get; set; }
    }
}
