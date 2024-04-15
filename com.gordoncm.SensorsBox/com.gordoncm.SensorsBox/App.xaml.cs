using System;
using com.gordoncm.SensorsBox.Database;
using Xamarin.Forms;

namespace com.gordoncm.SensorsBox
{
    public partial class App : Application
    {
        public App()
        {
            var provider = new Dependencies().Load().GetServiceProvider(); 
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {

            // Fetch user settings from the database
            CryptoDB database = await CryptoDB.Instance;
            string primaryColorName = await database.GetUserPrimaryColorAsync();
            string secondaryColorName = await database.GetUserSecondaryColorAsync();
            string fontSizeName = await database.GetUserFontSizeAsync();

            Color primaryColor = GetColorFromName(primaryColorName);
            Color secondaryColor = GetColorFromName(secondaryColorName);
            double fontSize = GetFontSizeFromName(fontSizeName);

            // Update the global styles
            ((Style)Resources["DynamicButtonStyle"]).Setters.Add(new Setter { Property = Button.BackgroundColorProperty, Value = secondaryColor });
            ((Style)Resources["DynamicLabelStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = primaryColor });
            ((Style)Resources["DynamicLabelStyle"]).Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = fontSize });
            ((Style)Resources["DynamicButtonStyle"]).Setters.Add(new Setter { Property = Button.FontSizeProperty, Value = fontSize });
            ((Style)Resources["DynamicPickerStyle"]).Setters.Add(new Setter { Property = Picker.FontSizeProperty, Value = fontSize });
            ((Style)Resources["DynamicEntryStyle"]).Setters.Add(new Setter { Property = Entry.FontSizeProperty, Value = fontSize });

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
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