using com.gordoncm.SensorsBox.ViewModels; 

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
            BindingContext = coinViewModel = new CoinViewModel(); 


        }
    }
}