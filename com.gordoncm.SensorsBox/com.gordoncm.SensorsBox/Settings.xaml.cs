using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.gordoncm.SensorsBox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        SettingsViewModel vm; 

        public Settings()
        {

            InitializeComponent();
            BindingContext = vm = new SettingsViewModel(); 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var cryptoDb = await CryptoDB.Instance;
            var user = cryptoDb.GetUserAsync().Result;

            vm.User = user;
        }

        private double GetFontSizeFromName(string fontSize)
        {
            switch (fontSize)
            {
                case "Small":
                    return 12.0;
                case "Medium":
                    return 20.0;
                case "Large":
                    return 28.0;
                default:
                    return 16.0;
            }
        }

        private Color GetColorFromName(string colorName)
        {
            switch (colorName)
            {
                case "Red":
                    return Color.Red;
                case "Blue":
                    return Color.Blue;
                case "Green":
                    return Color.Green;
                case "Yellow":
                    return Color.Yellow;
                case "Purple":
                    return Color.Purple;
                case "Pink":
                    return Color.Pink;
                case "Orange":
                    return Color.Orange;
                case "Brown":
                    return Color.Brown;
                case "Black":
                    return Color.Black;
                case "White":
                    return Color.White;
                default:
                    return Color.Default;
            }
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var cryptoDb = new CryptoDB(); 

            try
            {
                cryptoDb.UpdateUserAsync(vm.User);
            }
            catch
            {

            }
        }
    }
}