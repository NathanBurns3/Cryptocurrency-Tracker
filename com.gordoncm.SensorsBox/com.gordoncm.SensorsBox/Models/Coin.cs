using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Coins")]
    public class Coin
    {
        public int CoinId {  get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
