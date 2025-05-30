using System.Xml.Linq;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";

            var keys = new string[] { "Novelist", "BestWork", "Born" };

            foreach (string key in keys) {
                Console.WriteLine(ToJapanese(key) + ":" + Getitem(line, key));
            }

        }

        /// <summary>
        /// 引数の単語を日本語へ変換します
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {

            switch (key) {
                case "Novelist":
                    return "作家";
                case "BestWork":
                    return "代表作";
                case "Born":
                    return "誕生年";
            }
            return  "";
        }

        static string Getitem(string line, string key) {

            //キーを探し、対応する単語の開始位置を計算
            var startindex = line.IndexOf(key + "=") + key.Length + 1;

            //開始位置から終了位置を検索
            var endindex = line.IndexOf(";", startindex);

            //終了位置によって単語の長さを取得
            var length = endindex >= 0 ? (endindex - startindex) : (line.Length - startindex);


            //開始位置が-1だったら見つからなかった
            return startindex >= 0 ? line.Substring(startindex, length) : "";
        }
    }
}