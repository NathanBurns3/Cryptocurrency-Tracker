using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Text;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("portfolio")]
    public class Portfolio
    {
        [PrimaryKey]
        [AutoIncrement]
        public int PortfolioId { get; set; }

        public string CoinName { get; set; }

        public double CoinAmount { get; set; } 
    }
}
