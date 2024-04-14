using com.gordoncm.SensorsBox.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms.Xaml;

namespace com.gordoncm.SensorsBox.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CoinViewModel : INotifyPropertyChanged
    {
        private List<Coin> _items;
        public List<Coin> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public CoinViewModel()
        {
            Items = new List<Coin>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
