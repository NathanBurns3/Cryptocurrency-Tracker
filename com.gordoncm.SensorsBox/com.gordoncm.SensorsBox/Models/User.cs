using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace com.gordoncm.SensorsBox.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string WalletAddress { get; set; }
    }
}
