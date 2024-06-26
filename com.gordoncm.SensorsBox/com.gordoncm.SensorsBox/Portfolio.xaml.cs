﻿using com.gordoncm.SensorsBox.Database;
using com.gordoncm.SensorsBox.Models;
using com.gordoncm.SensorsBox.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace com.gordoncm.SensorsBox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Portfolio : ContentPage
    {
        PortfolioViewModel vm; 

        public Portfolio()
        {
            InitializeComponent();
            BindingContext = vm = new PortfolioViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}