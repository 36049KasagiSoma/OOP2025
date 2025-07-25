using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace RssReader {
    public class StaticEvent {
    private StaticEvent() { }

        /// <summary>
        /// コントロール内のラベルを取得します。
        /// </summary>
        public static List<Label> GetAllLabels(Control parent) {
            List<Label> labels = new List<Label>();

            foreach (Control control in parent.Controls) {
                if (control is Label label) {
                    labels.Add(label);
                }

                // 子コントロールを再帰的に検索
                if (control.HasChildren) {
                    labels.AddRange(GetAllLabels(control));
                }
            }

            return labels;
        }

        /// <summary>
        /// Jsonで保存します。
        /// </summary>
        public static T? LoadItem<T>(string filePath) {
            if (System.IO.File.Exists(filePath)) {
                string jsonText = System.IO.File.ReadAllText(filePath);
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
            System.IO.File.WriteAllText(filePath, jsonText);
        }

    }
}
