using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public static class InchConverter {
        private const double oneInchToMeter = 0.0254;

        /// <summary>インチをメートルに変換します。</summary>
        /// <param name="_inch">変換元インチ</param>
        /// <returns>変換後メートル</returns>
        public static double InchToMeter(int _inch) {
            double rtn = oneInchToMeter * _inch;
            return rtn;
        }
    }
}
