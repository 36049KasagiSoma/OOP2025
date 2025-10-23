using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerApp.Data {
    public class AddressResult {
        [JsonPropertyName("address1")]
        public string Address1 { get; set; } = string.Empty;

        [JsonPropertyName("address2")]
        public string Address2 { get; set; } = string.Empty;

        [JsonPropertyName("address3")]
        public string Address3 { get; set; } = string.Empty;

        [JsonPropertyName("kana1")]
        public string Kana1 { get; set; } = string.Empty;

        [JsonPropertyName("kana2")]
        public string Kana2 { get; set; } = string.Empty;

        [JsonPropertyName("kana3")]
        public string Kana3 { get; set; } = string.Empty;

        [JsonPropertyName("prefcode")]
        public string PrefCode { get; set; } = string.Empty;

        [JsonPropertyName("zipcode")]
        public string ZipCode { get; set; } = string.Empty;
    }

    public class AddressResponseItem {
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("results")]
        public List<AddressResult>? Results { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}
