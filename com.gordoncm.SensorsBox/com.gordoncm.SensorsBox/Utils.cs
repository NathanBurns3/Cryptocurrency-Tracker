using com.gordoncm.SensorsBox.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace com.gordoncm.SensorsBox
{
    public class Utils
    {
        public static decimal convert(string price)
        {
            decimal h = Decimal.Parse(
                    price,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture);

            return h; 
        }

        public static double convertToCad(string amount)
        {
            var cad = Convert.ToDouble(amount) * 1.38; 

            return cad;
        }

        public static double convertToPesos(string Price)
        {
            var pesos = Convert.ToDouble(Price) * 17.10; 

            return pesos;
        }

        public static int GetRowHeight(string FontSize)
        {
            int RowHeight = 50; 

            if (FontSize == "Small")
            {
                RowHeight = 50;
            }
            else if (FontSize == "Medium")
            {
                RowHeight = 80;
            }
            else if (FontSize == "Large")
            {
                RowHeight = 150;
            }

            return RowHeight; 
        }

        public static User createFakeUser()
        {
            var user = new User
            {
                UserId = 1,
                UserName = "DefaultUser",
                PreferedName = "Default User",
                ETHWalletAddress = "0x3f5ce5fbfe3e9af3971dd833d26ba9b5c936f0be",
                PrimaryColor = "Blue",
                SecondaryColor = "Orange",
                FontSize = "Small",
                Currency = "USD",
            };

            return user; 
        }

        public static int GetFontSize(string fontSize)
        {
            var size = 0;

            if (fontSize == "Small")
            {
                size = 12;
            }
            else if (fontSize == "Medium")
            {
                size = 20;
            }
            else if (fontSize == "Large")
            {
                size = 28;
            }

            return size; 
        }
    }
}
