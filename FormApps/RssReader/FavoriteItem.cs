using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader {
    public class FavoriteItem {
        public string Itemname { get; set; } = string.Empty;
        public string ItemUrl { get; set; } = string.Empty;

        //public override string ToString() {
        //    return Itemname;
        //}

        public override int GetHashCode() {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj) {
            //objがnullか、型が違うときは、等価でない
            if (obj == null || this.GetType() != obj.GetType()) {
                return false;
            }

            FavoriteItem c = (FavoriteItem)obj;
            return (this.Itemname == c.Itemname);
        }
    }
}
