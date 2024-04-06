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

        public string UserName { get; set; }

        public string PreferedName { get; set; }

        public string WalletAddress { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public string FontSize { get; set; }

        public string Currency { get; set; }
    }
}
