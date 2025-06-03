using System.Xml.Linq;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";

            var keys = GetKeys(line);

            foreach (string key in keys) {
                Console.WriteLine($"{ToJapanese(key)} : {Getitem(line, key)}");
            }

        }


        /// <summary>
        /// 引数の単語を日本語へ変換します。
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {
            return key switch {
                "Novelist" => "作家",
                "BestWork" => "代表作",
                "Born" => "誕生年",
                _ => ""
            };
        }

        /// <summary>
        /// 文字列から、キーに対応付けられたアイテムを返します。
        /// </summary>
        /// <param name="line">検索文字列</param>
        /// <param name="key">検索キー</param>
        /// <returns>キーに対応付けられたアイテム</returns>
        static string Getitem(string line, string key) {

            //キーを探し、対応する単語の開始位置を計算
            var startindex = line.IndexOf(key + "=") + key.Length + 1;

            //開始位置から終了位置を検索
            var endindex = line.IndexOf(';', startindex);

            //終了位置によって単語の長さを取得
            var length = endindex >= 0 ? (endindex - startindex) : (line.Length - startindex);

            //開始位置が(key.Length+1-1)だったら見つからなかった
            return startindex > key.Length ? line.Substring(startindex, length) : "";
        }

        /// <summary>
        /// 文字列中のキーを配列にして返します。
        /// </summary>
        /// <param name="line">検索文字列</param>
        /// <returns>含まれているキーの配列</returns>
        static string[] GetKeys(string line) {
            var startindex = 0;
            List<string> keys = new List<string>();
            do {
                int endindex = line.IndexOf('=', startindex);
                keys.Add(endindex >= 0 ? line.Substring(startindex, endindex - startindex) : line.Substring(startindex));
                startindex = line.IndexOf(';', endindex) + 1;
            } while (startindex > 0);

            return keys.ToArray();
        }
    }
}