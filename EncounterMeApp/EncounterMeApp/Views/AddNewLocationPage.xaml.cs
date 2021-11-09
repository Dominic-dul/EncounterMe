﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace EncounterMeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewLocationPage : ContentPage
    {
        public InternetLocationService service = new InternetLocationService();
        public AddNewLocationPage()
        {
            InitializeComponent();
            DisplayCurrentLocation();
        }

        private async void MyMap_MapClickedAsync(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            var xCoord = e.Position.Latitude;
            var yCoord = e.Position.Longitude;
            string action = await DisplayActionSheet("Do you want to add a location in these coordinates?", "Cancel", "Yes", $"X: {xCoord}", $"Y: {yCoord}");
            if (action == "Yes")
            {
                var name = await App.Current.MainPage.DisplayPromptAsync("Name of your location", "Name goes here");
                var points = await App.Current.MainPage.DisplayPromptAsync("Value of your location", "Points goes here");
                Random random = new Random();
                var newId = random.Next(100);
                var newLocation = new MyLocation{ NAME = name, points = Int32.Parse(points), positionX = xCoord, positionY = yCoord, owner = "NewOwner", Id = newId};
                await service.AddLocation(newLocation);
                //await LocationDatabase.AddLocation(xCoord, yCoord, Int32.Parse(points), name);
                
            }
            await Navigation.PopToRootAsync();
        }
        public async void DisplayCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromKilometers(5)));
        }
    }
}