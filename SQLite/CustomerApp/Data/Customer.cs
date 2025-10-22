using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomerApp.Data {
    class Customer {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// 住所
        /// </summary>
        public string Address { get; set; } = string.Empty;
        public byte[]? Picture { get; set; } = null;

        /// <summary>
        /// IdとPictureを除いた等価判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool EqualsParam(Object other) {
            if (other == null) return false;
            if (other.GetType() != typeof(Customer)) return false;
            Customer c = (Customer)other;

            return this.Name == c.Name &&
                this.Phone == c.Phone &&
                this.Address == c.Address;
        }
    }
}
