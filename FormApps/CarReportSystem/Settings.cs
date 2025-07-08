using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarReportSystem {
    public class Settings {
        public int MainFormBackColor { get; set; }

        private static Settings? instans;

        public static Settings GetInstans() {
            if (instans == null) {
                instans = new Settings();
            }
            return instans;
        }

        private Settings() { }// 外から呼ばれない
    }
}
