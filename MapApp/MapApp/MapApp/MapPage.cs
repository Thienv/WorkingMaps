using MapApp.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapApp
{
    public class MapPage : ContentPage
    {
        private double Latitude;
        private double Longitude;
        public MapPage()
        {
            GetLocation();
            
        }

        public async void GetLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync();
                Latitude = position.Latitude;
                Longitude = position.Longitude;
            }
            catch(Exception ex)
            {

            }
            
            CreateMap();
        }

        public void CreateMap()
        {
            var currentMap = new CustomMap
            {
                HasScrollEnabled = true,
                HasZoomEnabled = true,
                MapType = MapType.Street,
                WidthRequest = App.ScreenWidth,
                HeightRequest = App.ScreenHeight
            };

            var pin = new CustomPin
            {
                Type = PinType.Place,
                Address = "Linh Trung, Thu Duc, Ho Chi Minh",
                Label = "University Infomation of Technology",
                Position = new Position(10.879683, 106.803269),
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };

            var pin1 = new CustomPin
            {
                Type = PinType.Place,
                Address = "Linh Trung, Thu Duc, Ho Chi Minh",
                Label = "University Infomation of Technology",
                Position = new Position(5.879683, 100.803269),
                Id = "Xamarin",
                Url = "http://xamarin.com/about/"
            };
            currentMap.CustomPins = new List<CustomPin> { pin,pin1 };

            currentMap.Pins.Add(pin);          

            //pin.Clicked += UnivercityInfoAndTech_clicked;


            currentMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude), 
                Distance.FromMiles(1.0)));

            
            Content = currentMap;
        }

        private void CurrentMap_focuesed(object sender, FocusEventArgs e)
        {
            throw new NotImplementedException();
        }

        //public void UnivercityInfoAndTech_clicked(object sender, EventArgs e)
        //{
        //    var selectedPin = (Pin)sender;
        //    DisplayAlert(selectedPin.Label, selectedPin.Address, "OK");
        //}
    }
}
