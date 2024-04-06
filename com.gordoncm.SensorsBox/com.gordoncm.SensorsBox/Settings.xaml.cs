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
    }
}