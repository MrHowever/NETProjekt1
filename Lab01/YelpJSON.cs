using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class YelpJSON
    {
        public class RootObject
        {
            public int total { set; get; }
            public Business[] businesses { set; get; }
            public Region region { set; get; }
        }

        public class Business
        {
            public Category[] categories { set; get; }
            public Coordinates coordinates { set; get; }
            public String display_phone { set; get; }
            public double distance { set; get; }
            public String id { set; get; }
            public String alias { set; get; }
            public String image_url { set; get; }
            public bool is_closed { set; get; }
            public Location location { set; get; }
            public String name { set; get; }
            public String phone { set; get; }
            public String price { set; get; }
            public double rating { set; get; }
            public int review_count { set; get; }
            public String url { set; get; }
            public String[] transactions { set; get; }
        }

        public class Location
        {
            public String address1 { set; get; }
            public String address2 { set; get; }
            public String address3 { set; get; }
            public String city { set; get; }
            public String country { set; get; }
            public String[] display_address { set; get; }
            public String state { set; get; }
            public String zip_code { set; get; }
        }

        public class Category
        {
            public String alias { set; get; }
            public String title { set; get; }
        }

        public class Coordinates
        {
            public double latitude { set; get; }
            public double longitude { set; get; }
        }

        public class Region
        {
            public Center center { set; get; }
        }

        public class Center
        {
            public double latitude { set; get; }
            public double longitude { set; get; }
        }
    }
}
