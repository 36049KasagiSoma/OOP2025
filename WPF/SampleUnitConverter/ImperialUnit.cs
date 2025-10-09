using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUnitConverter {
    class ImperialUnit : DistanceUnit {
        private static List<ImperialUnit> units = new List<ImperialUnit> {
            new() { Name = "in", Coefficent = 1, },
            new() { Name = "ft", Coefficent = 12, },
            new() { Name = "yd", Coefficent = 12 * 3, },
            new() { Name = "ml", Coefficent = 12 * 3 * 1760, }
        };

        public static ICollection<ImperialUnit> Units => units.AsReadOnly();

        /// <summary>
        /// メートル単位からヤード単位に変換
        /// </summary>
        /// <param name="unit">変換元単位</param>
        /// <param name="value">変換値</param>
        /// <returns>変換後値</returns>
        public double FromMetricUnit(MetricUnit unit, double value)
            => value * unit.Coefficent / 25.4 / Coefficent;
    }
}
