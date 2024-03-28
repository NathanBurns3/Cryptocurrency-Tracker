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
    }
}