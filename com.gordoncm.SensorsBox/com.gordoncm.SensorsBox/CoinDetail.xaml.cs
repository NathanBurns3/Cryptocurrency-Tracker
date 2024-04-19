using com.gordoncm.SensorsBox.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using com.gordoncm.SensorsBox.Models;

namespace com.gordoncm.SensorsBox
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoinDetail : ContentPage
	{
		CoinDetailViewModel vm;
		private Coin _coin;  

		public CoinDetail (Coin coin)
		{
			InitializeComponent(); 
			_coin = coin;
			BindingContext = vm = new CoinDetailViewModel(_coin); 
		} 
	}
}