using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public class DistanceConverter {
        public ConverterBase From { get; init; }
        public ConverterBase To { get; init; }

        public DistanceConverter(ConverterBase from,ConverterBase to) { 
            From = from;
            To = to;
        }

        public double Convert(double value) {
            var metter = From.ToMeter(value);   // メーターへ変換
            return To.FromMeter(metter);        // 単位へ変換
        }
    }
}
