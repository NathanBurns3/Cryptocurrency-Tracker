using com.gordoncm.SensorsBox.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class CoinViewModel : BaseViewModel
    {
        public ObservableCollection<Coin> Items { get; set; }
        public CoinViewModel() 
        {
            Items = new ObservableCollection<Coin>(); 
        } 
    }
}
