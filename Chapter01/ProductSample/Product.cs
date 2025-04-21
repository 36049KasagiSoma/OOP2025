using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSample {
    //商品クラス
    public class Product {
        /// <summary>商品コード</summary>///
        public int Code { get; private set; }
        /// <summary>商品名</summary>///
        public string Name { get; private set; }
        /// <summary>商品価格（税抜き）</summary>///
        public int Price { get; private set; }

        private readonly double _taxRate = 0.1;


        public Product(int code, string name, int price) {
            this.Code = code;
            this.Name = name;
            this.Price = price;
        }

        /// <summary>消費税額を返します。</summary>
        /// <returns>この商品の消費税額</returns>
        public int GetTax() {
            return (int)(Price * _taxRate);
        }

        /// <summary>消費税込みの商品価格を返します。</summary>
        /// <returns>税込み価格</returns>
        public int GetPriceIncludingTax() {
            return Price + GetTax();
        }


        //お決まりオーバーライド群
        public override string ToString() {
            return $"{Code.ToString("00000")}:[商品名]{Name,-14}[税抜価格]{Price,-6}";
        }
        public override bool Equals(object? obj) {
            if (obj is Product p) {
                return p.Code == Code && p.Name == Name && p.Price == Price;
            }
            return false;
        }
        public override int GetHashCode() {
            int tmp = Code + Price + (String.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode());
            return tmp.GetHashCode();
        }
    }
}
