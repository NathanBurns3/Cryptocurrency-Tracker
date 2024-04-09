using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using com.gordoncm.SensorsBox.Models;
using Xamarin.Forms;
using com.gordoncm.SensorsBox.Database;
using System.Windows.Input;
using System;

namespace com.gordoncm.SensorsBox.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _availableColors;
        public ObservableCollection<string> AvailableColors
        {
            get { return _availableColors; }
            set
            {
                _availableColors = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _availableFontSizes;
        public ObservableCollection<string> AvailableFontSizes
        {
            get { return _availableFontSizes; }
            set
            {
                _availableFontSizes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _availableCurrency;
        public ObservableCollection<string> AvailableCurrency
        {
            get { return _availableCurrency; }
            set
            {
                _availableCurrency = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            // Initialize Available Colors
            AvailableColors = new ObservableCollection<string>
            {
                "Red",
                "Blue",
                "Green",
                "Orange",
                "Yellow",
                "Purple",
                "Gray",
            };

            // Initialize Available Font Sizes
            AvailableFontSizes = new ObservableCollection<string>
            {
                "Small",
                "Medium",
                "Large",
            };

            // Initialize Available Currency
            AvailableCurrency = new ObservableCollection<string>
            {
                "USD",
                "CAD",
                "Pesos",
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
