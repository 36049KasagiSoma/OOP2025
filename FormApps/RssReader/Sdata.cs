using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader {
    public class Sdata {
        public int OddBackColor { get; set; } = Color.White.ToArgb();
        public int EvenBackColor { get; set; } = Color.FromArgb(200, 200, 200).ToArgb();
        public int BackBackColor { get; set; } = Form.DefaultBackColor.ToArgb();
        public int TextBackColor { get; set; } = Color.Black.ToArgb();
        public int TimeOutValue { get; set; } = 60;
    }
}
