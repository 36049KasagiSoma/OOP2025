using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUnitConverter {
    class MetricUnit : DistanceUnit {
        private static List<MetricUnit> units = new List<MetricUnit> {
            new() { Name = "mm", Coefficent = 1, },
            new() { Name = "cm", Coefficent = 10, },
            new() { Name = "m", Coefficent = 10 * 100, },
            new() { Name = "km", Coefficent = 10 * 100 * 1000, }
        };

        public static ICollection<MetricUnit> Units => units.AsReadOnly();
        /// <summary>
        /// ヤード単位からメートル単位に変換
        /// </summary>
        /// <param name="unit">変換元単位</param>
        /// <param name="value">変換値</param>
        /// <returns>変換後値</returns>
        public double FromImperialUnit(ImperialUnit unit, double value)
            => value * unit.Coefficent * 25.4 / Coefficent;
    }
}
