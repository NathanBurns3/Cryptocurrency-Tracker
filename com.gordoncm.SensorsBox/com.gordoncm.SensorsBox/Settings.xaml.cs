using System;
using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.ViewModels;
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

            CryptoDB database = await CryptoDB.Instance;
            User user = await database.GetUserAsync();
            vm.User = user;
        }

        async void SaveUser(object sender, EventArgs e)
        {
            CryptoDB database = await CryptoDB.Instance;
            await database.UpdateUserAsync(vm.User);
            await Navigation.PopAsync();

            string primaryColorName = await database.GetUserPrimaryColorAsync();
            string secondaryColorName = await database.GetUserSecondaryColorAsync();
            string fontSizeName = await database.GetUserFontSizeAsync();

            Color primaryColor = GetColorFromName(primaryColorName);
            Color secondaryColor = GetColorFromName(secondaryColorName);
            double fontSize = GetFontSizeFromName(fontSizeName);

            WalletAddressLabel.TextColor = primaryColor;
            UsernameLabel.TextColor = primaryColor;
            PreferredNameLabel.TextColor = primaryColor;
            ColorPickerLabel.TextColor = primaryColor;
            FontSizeLabel.TextColor = primaryColor;
            CurrencyLabel.TextColor = primaryColor;

            WalletAddressLabel.FontSize = fontSize;
            UsernameLabel.FontSize = fontSize;
            PreferredNameLabel.FontSize = fontSize;
            ColorPickerLabel.FontSize = fontSize;
            FontSizeLabel.FontSize = fontSize;
            CurrencyLabel.FontSize = fontSize;

            WalletAddressEntry.FontSize = fontSize;
            UsernameEntry.FontSize = fontSize;
            PreferredNameEntry.FontSize = fontSize;

            PrimaryColorPicker.FontSize = fontSize;
            SecondaryColorPicker.FontSize = fontSize;
            FontSizePicker.FontSize = fontSize;
            CurrencyPicker.FontSize = fontSize;

            SaveButton.BackgroundColor = secondaryColor;
            SaveButton.FontSize = fontSize;

            OnAppearing();
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
    }
}