using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using MapSample;
using MapSample.Droid;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Shapes;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapSample.Droid
{
    class CustomMapRenderer : MapRenderer
    {
        List<CustomPin> customPins;
        //private GoogleMap _map;
        //List<Point> points = new List<Point>();
        MapView _map;
        GoogleMap _googleMap;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _map.Dispose();
                //_map.Touch -= _map_Touch;
                _googleMap.Clear();
                _googleMap.Dispose();
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                _map = Control;
                _map.SetOnTouchListener(new DroidTouchListener());
                //_map.Touch += _map_Touch1;
                //_map.GetMapAsync(this);
                _map.Touch += _map_Touch;
            }

            //if (e.OldElement != null)
            //{
            //    _map.Clear();
            //    _map.Dispose();
            //    _map.MapLongClick -= Map_MapLongClick;
            //}

            //if (e.NewElement != null)
            //{
            //    var formsMap = (CustomMap)e.NewElement;
            //}
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return base.OnTouchEvent(e);
        }

        protected override void OnMapReady(GoogleMap googleMap)
        {
            base.OnMapReady(googleMap);
            _googleMap = googleMap;
        }

        private void _map_Touch(object sender, TouchEventArgs e)
        {
            //var point = _googleMap.Projection.ToScreenLocation(e.Event.GetX(), e.Event.GetY());
            var s = e.Event.GetX();
            var ss = e.Event.GetY();
            //var point = _googleMap.Projection.ToScreenLocation()
            //var latLng = _googleMap.Projection.FromScreenLocation(point);

            //switch (e.Event.Action)
            //{
            //    case MotionEventActions.Down:
            //        _path = new Path();
            //        _path.MoveTo(point.X, point.Y);
            //        break;
            //    case MotionEventActions.Move:
            //        _path.LineTo(point.X, point.Y);
            //        break;
            //    case MotionEventActions.Up:
            //        _polygon = _map.AddPolygon(new PolygonOptions().InvokeColor(Color.Red.ToAndroid()).InvokeGeodesic(true).AddAll(_path));
            //        break;
            //}
        }

        //private void Map_Touch(object sender, Android.Gestures.GestureDetector.TouchEventArgs e)
        //{
        //    var point = _map.Projection.ToScreenLocation(e.Event.GetX(), e.Event.GetY());
        //    var latLng = _map.Projection.FromScreenLocation(point);

        //    switch (e.Event.Action)
        //    {
        //        case MotionEventActions.Down:
        //            _path = new Path();
        //            _path.MoveTo(point.X, point.Y);
        //            break;
        //        case MotionEventActions.Move:
        //            _path.LineTo(point.X, point.Y);
        //            break;
        //        case MotionEventActions.Up:
        //            _polygon = _map.AddPolygon(new PolygonOptions().InvokeColor(Color.Red.ToAndroid()).InvokeGeodesic(true).AddAll(_path));
        //            break;
        //    }
        //}

        //protected override void OnMapReady(GoogleMap map)
        //{
        //    base.OnMapReady(map);
        //    _map = map;
        //    _map.MapLongClick += Map_MapLongClick;
        //}

        //private void Map_MapLongClick(object sender, GoogleMap.MapLongClickEventArgs e)
        //{
        //    Point point = new Point(e.Point.Latitude, e.Point.Longitude);
        //    points.Add(point);

        //    if (points.Count > 2)
        //    {
        //        DrawPolygon();
        //    }
        //}

        //private void DrawPolygon()
        //{
        //    PolygonOptions options = new PolygonOptions();

        //    foreach (Point point in points)
        //    {
        //        options.Add(new LatLng(point.X, point.Y));
        //    }

        //    _map.AddPolygon(options);
        //}

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    if (e.PropertyName == "Points")
        //    {
        //        DrawPolygon();
        //    }
        //}

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();

            //var customPin = GetCustomPin(pin.Position.Latitude, pin.Position.Longitude);
            //if (customPin == null)
            //    return null;

            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            if (pin.Type == PinType.Place)
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
            }
            //if (customPin.IsTree == false)
            //{
            //    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));
            //}
            return marker;
        }

        CustomPin GetCustomPin(double lat, double longitude)
        {
            var position = new Position(lat, longitude);
            if (customPins != null)
            {
                foreach (var pin in customPins)
                {
                    if (pin.Position == position)
                    {
                        return pin;
                    }
                }
            }
            return null;
        }


        //public Android.Views.View GetInfoContents(Marker marker)
        //{
        //    throw new NotImplementedException();
        //}

        //public Android.Views.View GetInfoWindow(Marker marker)
        //{
        //    throw new NotImplementedException();
        //}
    }

    public class DroidTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            
            return false;
        }
    }
}