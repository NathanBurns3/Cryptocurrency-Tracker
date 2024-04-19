using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Coins")]
    public class Coin : BaseModel
    {
        public int CoinId {  get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string CMCRank { get; set; }

        public string CirculatingSupply { get; set; }

        public string TotalSupply { get; set; }

        public string MaxSupply { get; set; }

        public decimal? SENotation { get; set; }  

        // QUARTZ.NET 
    }
}
