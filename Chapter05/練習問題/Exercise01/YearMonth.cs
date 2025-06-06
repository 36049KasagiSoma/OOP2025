using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    public class YearMonth {
        public readonly int Year;
        public readonly int Month;

        public bool Is21Century() => Year > 2000 && Year <= 2100;

        public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }

        
    }
}
