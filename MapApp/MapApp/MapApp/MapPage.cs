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
            var openLocation = new Button
            {
                Text = "Search location"
            };
            openLocation.Clicked += (sender, e) => {

                if (Device.RuntimePlatform == Device.iOS)
                {
                    //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                    Device.OpenUri(new Uri("http://maps.apple.com/?q=394+Pacific+Ave+San+Francisco+CA"));
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    // opens the Maps app directly
                    Device.OpenUri(new Uri("geo:0,0?q=394+Pacific+Ave+San+Francisco+CA"));

                }
                else if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.OpenUri(new Uri("bingmaps:?where=394 Pacific Ave San Francisco CA"));
                }
            };

            
            
            Content = new StackLayout
            {
                Padding = new Thickness(5, 20, 5, 0),
                HorizontalOptions = LayoutOptions.Fill,
                Children = {
                    
                    openLocation
                    
                }
            };
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
