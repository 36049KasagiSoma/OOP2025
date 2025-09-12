using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ColorChecker {
    public class ObjectSaveAndLoad {
        private ObjectSaveAndLoad() { }

        // 仮置き(鍵をべた書きするのは本来非推奨)
        private static string key = "XsjysrbhHip6L4dp";

        /// <summary>
        /// Jsonで保存します。
        /// </summary>
        public static T LoadItem<T>(string filePath) {
            if (System.IO.File.Exists(filePath)) {
                string jsonText = Encryption.DecryptString(System.IO.File.ReadAllText(filePath),key);
                return JsonSerializer.Deserialize<T>(jsonText,
                    new JsonSerializerOptions {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    }
                );
            }
            return default(T);
        }

        /// <summary>
        /// Jsonを読み込みます。
        /// </summary>
        public static void SaveItem(string filePath, object item) {
            string jsonText = JsonSerializer.Serialize(item,
                new JsonSerializerOptions {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true
                });
            System.IO.File.WriteAllText(filePath, Encryption.EncryptString(jsonText,key));
        }
    }
}
