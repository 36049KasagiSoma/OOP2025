using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter {
    public abstract class ConverterBase {
        /// <summary>指定された単位が自分のものか判定</summary>
        public abstract bool IsMyUnit(string unit);
        /// <summary>メートルとの比率</summary>
        protected abstract double Ratio { get; }
        /// <summary>距離の単位名</summary>
        public abstract string UnitName { get; }
        /// <summary>
        /// メートルからの変換
        /// </summary>
        /// <param name="meter">変換元メートル値</param>
        /// <returns>変換した値</returns>
        public double FromMeter(double meter) => meter / Ratio;
        /// <summary>
        /// メートルへの変換
        /// </summary>
        /// <param name="feet">変換元値</param>
        /// <returns>変換先メートル値</returns>
        public double ToMeter(double feet) => feet * Ratio;
    }
}
