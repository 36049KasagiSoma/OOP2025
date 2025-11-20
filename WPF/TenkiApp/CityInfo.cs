using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    public class CityInfo {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double MapX { get; set; }
        public double MapY { get; set; }

        public CityInfo(double lat, double lon) {
            Latitude = lat;
            Longitude = lon;
        }
    }
}
