using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenkiApp {
    public class ForecastItem {
        public string Date { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MaxTemp { get; set; } = string.Empty;
        public string MinTemp { get; set; } = string.Empty;
        public string WeatherImage { get; set; } = string.Empty;
    }
}
