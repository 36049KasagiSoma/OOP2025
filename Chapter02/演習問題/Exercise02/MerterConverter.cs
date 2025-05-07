using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    /// <summary>
    /// メートルから各単位もしくは、各単位からメートルに変換するメソッドを持つ静的クラスです。
    /// </summary>
    public static class MerterConverter {
        private const double oneInchToMeter = 0.0254;
        private const double oneYardToMeter = 0.9144;

        /// <summary>インチをメートルに変換します。</summary>
        /// <param name="_inch">変換元インチ</param>
        /// <returns>変換後メートル</returns>
        public static double InchToMeter(int _inch) {
            double rtn = oneInchToMeter * _inch;
            return rtn;
        }

        /// <summary>メートルをインチに変換します。</summary>
        /// <param name="_meter">変換元メートル</param>
        /// <returns>変換後ヤード</returns>
        public static double MeterToInch(int _meter) {
            double rtn = _meter / oneInchToMeter;
            return rtn;
        }


        /// <summary>ヤードをメートルに変換します。</summary>
        /// <param name="_yard">変換元ヤード</param>
        /// <returns>変換後メートル</returns>
        public static double YardToMeter(int _yard) {
            double rtn = oneYardToMeter * _yard;
            return rtn;
        }

        /// <summary>メートルをヤードに変換します。</summary>
        /// <param name="_meter">変換元インチ</param>
        /// <returns>変換後ヤード</returns>
        public static double MeterToYard(int _meter) {
            double rtn = _meter / oneYardToMeter;
            return rtn;
        }
    }
}