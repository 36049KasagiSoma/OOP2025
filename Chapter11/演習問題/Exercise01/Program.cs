using System.Text.RegularExpressions;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine(IsPhoneNumber("080-9111-1234"));
            Console.WriteLine(IsPhoneNumber("090-9111-1234"));
            Console.WriteLine(IsPhoneNumber("060-9111-1234"));
            Console.WriteLine(IsPhoneNumber("190-9111-1234"));
            Console.WriteLine(IsPhoneNumber("091-9111-1234"));
            Console.WriteLine(IsPhoneNumber("090-9111-12341"));
            Console.WriteLine(IsPhoneNumber("A091-9111-1234"));
            Console.WriteLine(IsPhoneNumber("091-911-1234"));
            Console.WriteLine(IsPhoneNumber("091-9111-234"));
            // 追加分
            Console.WriteLine(IsPhoneNumber("060-1234-5678"));    // false: 先頭が携帯番号ではない
            Console.WriteLine(IsPhoneNumber("100-1234-5678"));    // false: 先頭番号不正
            Console.WriteLine(IsPhoneNumber("091-1234-5678"));    // false: 091は一般的に携帯ではない
            Console.WriteLine(IsPhoneNumber("080-123-5678"));     // false: 中央ブロックが3桁
            Console.WriteLine(IsPhoneNumber("080-12345-5678"));   // false: 中央ブロックが5桁
            Console.WriteLine(IsPhoneNumber("080-1234-567"));     // false: 最後のブロックが3桁
            Console.WriteLine(IsPhoneNumber("080-1234-56789"));   // false: 最後のブロックが5桁
            Console.WriteLine(IsPhoneNumber("08012345678"));      // false: ハイフンなし
            Console.WriteLine(IsPhoneNumber("080--1234-5678"));   // false: ハイフン多すぎ
            Console.WriteLine(IsPhoneNumber("080-1234--5678"));   // false: ハイフン位置間違い
            Console.WriteLine(IsPhoneNumber("080-1234-5678 "));   // false: 末尾にスペース
            Console.WriteLine(IsPhoneNumber(" 080-1234-5678"));   // false: 先頭にスペース
            Console.WriteLine(IsPhoneNumber("080-12a4-5678"));    // false: 中央ブロックに文字
            Console.WriteLine(IsPhoneNumber("080-1234-56b8"));    // false: 最後のブロックに文字
            Console.WriteLine(IsPhoneNumber("08O-1234-5678"));    // false: 数字ではなくO（オー）
            Console.WriteLine(IsPhoneNumber("080-1234-567８"));   // false: 全角数字含む
            Console.WriteLine(IsPhoneNumber("090_1234_5678"));    // false: ハイフンではなくアンダースコア
            Console.WriteLine(IsPhoneNumber("090 1234 5678"));    // false: ハイフンではなくスペース
            Console.WriteLine(IsPhoneNumber("090-123-45678"));    // false: 中央3桁、最後5桁
            Console.WriteLine(IsPhoneNumber("091-12345-6789"));   // false: 中央5桁
            Console.WriteLine(IsPhoneNumber("091-1234-678"));     // false: 最後3桁
            Console.WriteLine(IsPhoneNumber("999-9999-9999"));    // false: 不正な先頭番号
            Console.WriteLine(IsPhoneNumber("080-0000-000"));     // false: 最後3桁
            Console.WriteLine(IsPhoneNumber("080-0000-00000"));   // false: 最後5桁

        }

        static bool IsPhoneNumber(string s) {
            return Regex.IsMatch(s, @"^0[7-9]0(-[0-9]{4}){2}$");
        }
    }
}
