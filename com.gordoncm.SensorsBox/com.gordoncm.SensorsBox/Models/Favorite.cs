using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Favorites")]
    public class Favorite : BaseModel
    {
        public int Id { get; set; }

        public int CoinId { get; set; }
    }
}
