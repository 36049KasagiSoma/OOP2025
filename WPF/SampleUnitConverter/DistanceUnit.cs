using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUnitConverter {
    class DistanceUnit {
        public string Name { get; set; } = string.Empty;
        public double Coefficent { get; set; }
        public override string ToString() => Name;
    }
}
