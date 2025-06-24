using System.Globalization;
using System.Text;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var today = new DateTime(2025, 7, 12);
            var now = DateTime.Now;
            Console.WriteLine($"Today:{today.Month}");
            Console.WriteLine($"Now:{now}");

            //自分の生年月日は何曜日?
            Console.WriteLine("===============");
            var birthday = DateTime.Now;
            try {
                Console.WriteLine("日付を入力");
                Console.Write("西暦：");
                string? inY = Console.ReadLine();
                int y = int.Parse(string.IsNullOrEmpty(inY) ? "1900" : inY);
                Console.Write("月：");
                string? inM = Console.ReadLine();
                int m = int.Parse(string.IsNullOrEmpty(inM) ? "1" : inM);
                Console.Write("日：");
                string? inD = Console.ReadLine();
                int d = int.Parse(string.IsNullOrEmpty(inD) ? "1" : inD);

                birthday = new DateTime(y, m, d);

                Console.WriteLine();

                var cul = new CultureInfo("ja-JP");
                cul.DateTimeFormat.Calendar = new JapaneseCalendar();

                Console.WriteLine(birthday.ToString("ggyy年M月d日", cul) + "は" +
                    cul.DateTimeFormat.GetDayName(birthday.DayOfWeek) + "です。");
                Console.WriteLine($"生まれてから{(now.Date - birthday.Date).Days}日です");

                //閏年の判別
                Console.WriteLine("===============");
                Console.WriteLine(DateTime.IsLeapYear(birthday.Year) ? "閏年です" : "閏年ではありません");
            } catch (FormatException e) {
                Console.WriteLine("--------------------------");
                Console.WriteLine("変換エラー");
                Console.WriteLine(e.Message);
                Console.WriteLine("--------------------------");
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine("--------------------------");
                Console.WriteLine("実行時エラー");
                Console.WriteLine(e.Message);
                Console.WriteLine("--------------------------");

            }

            Console.WriteLine("===============");


            string wara = "＼（^o^）／　(＾▽＾)　(￣▽￣)ノ　(＾_＾)／　(⌒▽⌒)／　＼(＾0＾)／　(＾◇＾)ノ";

            while(true){
                now = DateTime.Now;
                Console.Write($"\r{wara}|  {now.ToString()}");
                wara = wara.Substring(1) + wara.Substring(0, 1);
                Thread.Sleep(100);
            }

        }

    }
}
