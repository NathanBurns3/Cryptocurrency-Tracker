using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace com.gordoncm.SensorsBox.Models
{
    [SQLite.Table("Favorites")]
    public class Favorite : BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CoinId { get; set; }
        public string Name { get; set; }
    }
}
