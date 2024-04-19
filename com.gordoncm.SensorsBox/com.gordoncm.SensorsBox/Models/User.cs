using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TableAttribute = SQLite.TableAttribute;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string PreferedName { get; set; }

        public string ETHWalletAddress { get; set; } 

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public string FontSize { get; set; }

        public string Currency { get; set; }
    }
}
