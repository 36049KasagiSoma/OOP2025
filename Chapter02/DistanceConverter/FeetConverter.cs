using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    public  class FeetConverter {
        private const double ratio = 0.3048;

        /// <summary>フィート値をメートル値に変換します。</summary>
        /// <param name="_feet">変換元フィート値</param>
        /// <returns>メートル値</returns>
        public  double FeetToMeter(int _feet) {
            return _feet * ratio;
        }

        /// <summary>メートル値をフィート値に変換します。</summary>
        /// <param name="_meter">変換元メートル値</param>
        /// <returns>フィート値</returns>
        public  double MeterToFeet(int _meter) {
            return _meter / ratio;
        }

       
    }
}
