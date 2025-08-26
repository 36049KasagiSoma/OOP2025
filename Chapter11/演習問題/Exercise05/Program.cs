using System.Text;
using System.Text.RegularExpressions;

namespace Exercise05 {
    internal class Program {
        static void Main(string[] args) {
            string text = File.ReadAllText("sample.html");

            // メモ
            // 1,置き換え元
            // 2,タグ名を取り出す正規表現
            // （「<」から始まり、「/」が0～1文字で、「>」「 」が出るまで)
            // 3,ラムダで、Matchを受け取り、それをLow化
            string newText = Regex.Replace(
                text,
                @"</?[^>\s]+",
                m => m.Value.ToLower()
            );

            File.WriteAllText("sampleOut.html", newText);

        }
    }
}
