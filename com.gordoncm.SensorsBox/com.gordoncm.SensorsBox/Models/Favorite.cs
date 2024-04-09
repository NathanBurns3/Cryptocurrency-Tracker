using SQLite;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Favorites")]
    public class Favorite
    {
        [PrimaryKey][AutoIncrement]
        public int Id { get; set; }

        public int CoinId { get; set; }
    }
}
