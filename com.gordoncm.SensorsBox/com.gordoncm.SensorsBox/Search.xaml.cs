﻿using com.gordoncm.SensorsBox.ViewModels;
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
    public partial class Search : ContentPage
    {
        private SearchViewModel vm; 

        public Search()
        {
            InitializeComponent();
            BindingContext = vm = new SearchViewModel();
        }
    }
}