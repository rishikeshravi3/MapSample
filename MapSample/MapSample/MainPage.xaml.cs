using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapSample
{
    public partial class MainPage : ContentPage
    {
        MapViewModel ViewModel { get; set; }

        List<LocationModel> Locations = new List<LocationModel>();
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new MapViewModel(LocationMap);

            //Locations.Add(new LocationModel
            //{
            //    Latitude = 19.06587539922634,
            //    Longitude = 72.83357594603224,
            //    Label = "Mitron",
            //    Address = "Bandra",
            //    FullAddress = "Mitron - Bandra"
            //});

            //Locations.Add(new LocationModel
            //{
            //    Latitude = 19.19790167049828,
            //    Longitude = 72.94957936931199,
            //    Label = "Mitron",
            //    Address = "Thane",
            //    FullAddress = "Mitron - Thane"
            //});

            //Locations.Add(new LocationModel
            //{
            //    Latitude = 19.2078709987473,
            //    Longitude = 72.86564962003868,
            //    Label = "Mitron",
            //    Address = "Kandivali",
            //    FullAddress = "Mitron - Kandivali"
            //});

            //foreach (var item in Locations)
            //{
            //    Location location = new Location(item.Latitude, item.Longitude);
            //    Pin pin = new Pin
            //    {
            //        Label = item.Label,
            //        Address = item.Address,
            //        Type = PinType.Place,
            //        Position = new Position(location.Latitude, location.Longitude)
            //    };
            //    pin.BindingContext = item;
            //    pin.MarkerClicked += Pin_MarkerClicked;
            //    LocationMap.Pins.Add(pin);
            //}

            //LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Locations[0].Latitude, Locations[0].Longitude), Distance.FromKilometers(10)));
        }

        private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            e.HideInfoWindow = true;
            var s = (sender as Pin).BindingContext as LocationModel;
            await DisplayAlert("Pin Clicked", $"{s.FullAddress} was clicked.", "Ok");
        }

        private async void Pin_InfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            var s = (sender as Pin).BindingContext as LocationModel;
            await DisplayAlert("Pin Clicked", $"{s.FullAddress} was clicked.", "Ok");
        }

        private void LocationMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            LocationMap.Pins.Add(new CustomPin
            {
                Label = "Santa Cruz",
                Address = "The city with a boardwalk",
                Position = new Position(e.Position.Latitude, e.Position.Longitude),
                Type = PinType.Place
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var points = LocationMap.Pins.Where(x => x.Type == PinType.Place).ToList();
            foreach (var item in points)
            {
                LocationMap.Pins.Remove(item);
            }
            LocationMap.MapElements.Clear();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var points = LocationMap.Pins.Where(x => x.Type == PinType.Place).Select(x => x.Position).ToList();

            Polygon polygon = new Polygon
            {
                StrokeWidth = 8,
                StrokeColor = Color.Black,
                FillColor = Color.LightBlue
            };

            foreach (var item in points)
            {
                polygon.Geopath.Add(item);
            }
            polygon.ClassId = "SelectedRegion";
            LocationMap.MapElements.Add(polygon);
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            var pointsInsidePolygon = new List<LocationModel>();
            var s = LocationMap.MapElements.Where(x => x.ClassId == "SelectedRegion").FirstOrDefault();
            var polygon = LocationMap.MapElements.Where(x => x.ClassId == "SelectedRegion").FirstOrDefault() as Polygon;
            foreach (var item in ViewModel.Locations)
            {
                bool IsInside = IsPointInPolygon(item.PositionOnMap, polygon.Geopath.ToArray());
                if (IsInside)
                {
                    pointsInsidePolygon.Add(item);
                }
            }
            var msg = "";
            foreach (var item in pointsInsidePolygon)
            {
                msg += item.FullAddress + "\n";
            }
            await DisplayAlert("Points Inside", msg, "Ok");
        }

        public bool IsPointInPolygon(Position p, Position[] polygon)
        {
            double minX = polygon[0].Latitude;
            double maxX = polygon[0].Latitude;
            double minY = polygon[0].Longitude;
            double maxY = polygon[0].Longitude;
            for (int i = 1; i < polygon.Length; i++)
            {
                Position q = polygon[i];
                minX = Math.Min(q.Latitude, minX);
                maxX = Math.Max(q.Latitude, maxX);
                minY = Math.Min(q.Longitude, minY);
                maxY = Math.Max(q.Longitude, maxY);
            }
            if (p.Latitude < minX || p.Latitude > maxX || p.Longitude < minY || p.Longitude > maxY)
            {
                return false;
            }

            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((polygon[i].Longitude > p.Longitude) != (polygon[j].Longitude > p.Longitude) &&
                     p.Latitude < (polygon[j].Latitude - polygon[i].Latitude) * (p.Longitude - polygon[i].Longitude) / (polygon[j].Longitude - polygon[i].Longitude) + polygon[i].Latitude)
                {
                    inside = !inside;
                }
            }
            return inside;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //if (LocationMap.i.IsClickable)
            //{
            //    map.IsClickable = false;
            //}
            //else
            //{
            //    map.IsClickable = true;
            //}
        }
    }
}
