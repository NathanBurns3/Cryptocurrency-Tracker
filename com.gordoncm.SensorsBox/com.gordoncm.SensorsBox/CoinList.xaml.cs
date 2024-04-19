using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.ViewModels;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.gordoncm.SensorsBox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoinList : ContentPage
    {
        CoinViewModel coinViewModel;
        public CoinList()
        { 
            InitializeComponent();
            BindingContext = coinViewModel = new CoinViewModel(Navigation); 
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Coin) e.SelectedItem; 
            await Navigation.PushAsync(new CoinDetail(item)); 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing(); 
        } 
    }
}