using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarReportSystem {
    [Serializable]
    public class CarRecord {
        [DisplayName("日付")]
        public  DateTime Date { get; set; }
        [DisplayName("記録者")]
        public string Author { get; set; }=string.Empty;
        [DisplayName("メーカー")]
        public MakerGroup Maker { get; set; }
        [DisplayName("車名")]
        public string CarName { get; set; } = string.Empty;
        [DisplayName("レポート")]
        public string Report { get; set; } = string.Empty;
        [DisplayName("画像")]
        public Image? Picture { get; set; }
    }

    public enum MakerGroup {
        NONE = 0,
        TOYOTA = 1,
        NISSAN = 2,
        HONDA = 3,
        SUBARU = 4,
        IMPORT = 5,
        OTHER = 6,
    }
}
