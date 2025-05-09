using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCalculator {
    /// <summary>売り上げ集計クラス</summary>
    class SalesCounter {
        private readonly List<Sale> _sales;

        public SalesCounter(List<Sale> sales) {
            _sales = sales;
        }

        /// <summary>店舗名ごとに集計</summary>
        /// <returns>店舗ごとの売り上げデータ</returns>
        public Dictionary<string, int> GetPerStoreSales() {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(Sale sale in _sales) {
                if (dict.ContainsKey(sale.ShopName)) {
                    dict[sale.ShopName] += sale.Amount;
                } else {
                    dict[sale.ShopName] = sale.Amount;
                }
            }
            return dict;
        }

        /// <summary>カテゴリーごとに集計</summary>
        /// <returns>カテゴリーごとの売り上げデータ</returns>
        public Dictionary<string, int> GetPerProductCategory() {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (Sale sale in _sales) {
                if (dict.ContainsKey(sale.ProductCategory)) {
                    dict[sale.ProductCategory] += sale.Amount;
                } else {
                    dict[sale.ProductCategory] = sale.Amount;
                }
            }
            return dict;
        }
    }
}
