using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    public record YearMonth(int Y, int M) {
        public readonly int Year = Y;
        public readonly int Month = M;

        public bool Is21Century => Year > 2000 && Year <= 2100;

        public YearMonth AddOneMonth() {
            bool isUp = Month == 12;
            return isUp ? new YearMonth(Year + 1, 1) : new YearMonth(Year, Month + 1);
        }

        public override string ToString() => $"{Year}年{Month}月";
    }
}
