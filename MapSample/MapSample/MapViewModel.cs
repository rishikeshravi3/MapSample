using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapSample
{
    public class MapViewModel : BaseViewModel
    {
        private ObservableCollection<LocationModel> _Locations = new ObservableCollection<LocationModel>();
        public ObservableCollection<LocationModel> Locations
        {
            get { return _Locations; }
            set { SetProperty(ref _Locations, value); }
        }

        public CustomMap LocationMap { get; set; }

        public MapViewModel(CustomMap _LocationMap)
        {
            Title = "Hello Maps Test";

            LocationMap = _LocationMap;

            var obj = new LocationModel
            {
                Latitude = 19.06587539922634,
                Longitude = 72.83357594603224,
                Label = "Mitron",
                Address = "Bandra",
                FullAddress = "Mitron - Bandra"
            };
            obj.PositionOnMap = new Position(obj.Latitude, obj.Longitude);
            Locations.Add(obj);

            var obj1 = new LocationModel
            {
                Latitude = 19.19790167049828,
                Longitude = 72.94957936931199,
                Label = "Mitron",
                Address = "Thane",
                FullAddress = "Mitron - Thane"
            };
            obj1.PositionOnMap = new Position(obj1.Latitude, obj1.Longitude);
            Locations.Add(obj1);


            var obj2 = new LocationModel
            {
                Latitude = 19.2078709987473,
                Longitude = 72.86564962003868,
                Label = "Mitron",
                Address = "Kandivali",
                FullAddress = "Mitron - Kandivali"
            };
            obj2.PositionOnMap = new Position(obj2.Latitude, obj2.Longitude);
            Locations.Add(obj2);

            obj = new LocationModel
            {
                Latitude = 19.2079709987473,
                Longitude = 72.86164962003868,
                Label = "Mitron",
                Address = "Borivali",
                FullAddress = "Mitron - Borivali"
            };
            obj.PositionOnMap = new Position(obj.Latitude, obj.Longitude);
            Locations.Add(obj);

            //LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Locations[0].Latitude, Locations[0].Longitude), Distance.FromKilometers(10)));

            //// instantiate a polygon
            //Polygon polygon = new Polygon
            //{
            //    StrokeWidth = 8,
            //    StrokeColor = Color.Black,
            //    FillColor = Color.OrangeRed,
            //    Geopath =
            //    {
            //        new Position(Locations[0].Latitude, Locations[0].Longitude),
            //        new Position(Locations[1].Latitude, Locations[1].Longitude),
            //        new Position(Locations[2].Latitude, Locations[2].Longitude),
            //    }
            //};

            //// add the polygon to the map's MapElements collection
            //LocationMap.MapElements.Add(polygon);

            Setup();
        }

        private async void Setup()
        {
            Geocoder geoCoder = new Geocoder();

            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync("Waterloo, Ontario, Canada");
            Position position = approximateLocations.FirstOrDefault();

            //var obj = new LocationModel
            //{
            //    Latitude = position.Latitude,
            //    Longitude = position.Longitude,
            //    Label = "Waterloo",
            //    Address = "Ontario",
            //    FullAddress = "Waterloo - Ontario"
            //};
            //obj.PositionOnMap = new Position(obj.Latitude, obj.Longitude);
            //Locations.Add(obj);

            LocationMap.MoveToRegion(MapSpan.FromCenterAndRadius(Locations[0].PositionOnMap, Distance.FromKilometers(10)));
        }
    }

    public class LocationModel : BaseViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
        public string FullAddress { get; set; }
        public Position PositionOnMap { get; set; }

        //public Position _PositionOnMap;
        //public Position PositionOnMap
        //{
        //    get { return _PositionOnMap; }
        //    set { SetProperty(ref _PositionOnMap, value); }
        //}
    }
}
