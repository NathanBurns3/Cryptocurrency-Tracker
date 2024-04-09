using SQLite;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Coins")]
    public class Coin
    {
        [PrimaryKey][AutoIncrement]
        public int CoinId {  get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
