using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section04 {
    /// <summary>売上クラス</summary>
    class Sale {
        /// <summary>店舗名</summary>
        public string ShopName { get; set; } = String.Empty;
        /// <summary>商品カテゴリー</summary>
        public string ProductCategory { get; set; } = String.Empty;
        /// <summary>売上高</summary>
        public int Amount { get; set; }
    }
}
