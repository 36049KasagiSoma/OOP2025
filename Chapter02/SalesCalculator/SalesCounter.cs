﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCalculator {
    /// <summary>売り上げ集計クラス</summary>
    class SalesCounter {
        private readonly IEnumerable<Sale> _sales;

        //Listでもパスでも対応できるようオーバーライド
        public SalesCounter(IEnumerable<Sale> sales) {
            _sales = sales;
        }
        public SalesCounter(string filePath) {
            _sales = ReadSales(filePath);
        }

        private IEnumerable<Sale> ReadSales(string filePath) {
            //売り上げリスト
            var sales = new List<Sale>();
            var lines = File.ReadAllLines(filePath);
            //各行をカンマ区切りで分割、Saleデータの作成、登録
            foreach (var line in lines) {
                var items = line.Split(',');
                var sale = new Sale {
                    ShopName = items[0],
                    ProductCategory = items[1],
                    Amount = int.Parse(items[2])
                };
                sales.Add(sale);
            }
            return sales;
        }

        /// <summary>店舗名ごとに集計</summary>
        /// <returns>店舗ごとの売り上げデータ</returns>
        public IDictionary<string, int> GetPerStoreSales() {
            var dict = new Dictionary<string, int>();
            foreach (var sale in _sales) {
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
        public IDictionary<string, int> GetPerProductCategory() {
            var dict = new Dictionary<string, int>();
            foreach (var sale in _sales) {
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
