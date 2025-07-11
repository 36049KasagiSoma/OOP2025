using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarReportSystem {
    public class Settings {
        public int MainFormBackColor { get; set; }


        // 以下、シングルトンのためのやつ

        private static Settings? instans;

        public static Settings GetInstans() {
            if (instans is null) {
                instans = new Settings();
            }
            return instans;
        }

        private Settings() { }  // 外から呼ばれない
    }
}
