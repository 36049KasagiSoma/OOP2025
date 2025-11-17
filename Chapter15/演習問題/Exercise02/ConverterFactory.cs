using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public static class ConverterFactory {
        private readonly static ConverterBase[] _converters = [
            new MeterConverter(),
            new FeetConverter(),
            new YardConverter(),
            new InchConverter(),
            new MilleConverter(),
            new KilometerConverter(),
        ];

        public static ConverterBase? GetInstance(string name) =>
            _converters.FirstOrDefault(x=>x.IsMyUnit(name));
    }
}
