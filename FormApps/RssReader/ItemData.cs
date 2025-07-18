using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader {
    public class ItemData {
        public required string Title { get; set; }
        public required string Link { get; set; }
        public required DateTime PubDate { get; set; }

        public override string ToString() {
            return $"[{PubDate.ToString("yyyy/MM/dd HH:mm:dd")}] {Title}";
        }

    }
}
