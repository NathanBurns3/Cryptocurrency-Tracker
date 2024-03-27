using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.gordoncm.SensorsBox
{
    public class Constants
    {
        public static string DATABASE_NAME = "todo.db3";

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DATABASE_NAME);
            }
        }
    }
}
