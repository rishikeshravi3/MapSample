using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace MapSample
{
    public class CustomPin : Pin
    {
        public bool IsTree { get; set; } = true;
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
