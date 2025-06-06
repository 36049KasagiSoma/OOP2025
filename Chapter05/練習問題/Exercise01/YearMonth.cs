using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    public class YearMonth {
        public readonly int Year;
        public readonly int Month;

        public bool Is21Century => Year > 2000 && Year <= 2100;

        public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }

        public YearMonth AddOneMonth() {
            bool isUp = Month == 12;
            return isUp ? new YearMonth(Year + 1, 1) : new YearMonth(Year, Month + 1);
        }

        public override string ToString() => $"{Year}年{Month}月";
    }
}
