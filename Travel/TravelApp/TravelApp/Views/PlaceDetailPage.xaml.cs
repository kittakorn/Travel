﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using TravelApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceDetailPage : ContentPage
    {
        internal PlaceDetailPage(Place place)
        {
            InitializeComponent();
            Title = "รายละเอียด";
            var placeDetail = new PlaceDetailViewModel { Place = place };
            BindingContext = placeDetail;
        }
        protected override bool OnBackButtonPressed()
        {
            var mainPage = new MainPage() as TabbedPage;
            mainPage.CurrentPage = mainPage.Children[0];
            Application.Current.MainPage = mainPage;
            return true;
        }



    }
}