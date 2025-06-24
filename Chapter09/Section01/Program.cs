using System.Globalization;
using System.Text;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var today = new DateTime(2025, 7, 12);
            var now = DateTime.Now;
            Console.WriteLine($"Today:{today}");
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

                Console.WriteLine("===============");


                var cul = new CultureInfo("ja-JP");
                cul.DateTimeFormat.Calendar = new JapaneseCalendar();

                Console.WriteLine(birthday.ToString("ggyy年M月d日", cul) + "は" +
                    cul.DateTimeFormat.GetDayName(birthday.DayOfWeek) + "です。");
                int totalDays = (now.Date - birthday.Date).Days;
                Console.WriteLine($"生まれてから{totalDays}日です");
                Console.WriteLine();


                //閏年の判別
                Console.WriteLine("===============");
                Console.WriteLine(DateTime.IsLeapYear(birthday.Year) ? "閏年です。" : "平年です。");
                Console.WriteLine();

                Console.WriteLine("===============");

                int work = now.Year - birthday.Year;
                int age = work - (now < birthday.AddYears(work) ? 1 : 0);
                Console.WriteLine($"あなたは{age}歳です。");
                Console.WriteLine();

                Console.WriteLine("===============");
                Console.WriteLine($"今年の1月1日から、{now.DayOfYear}日です。");
                Console.WriteLine();
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



            while (true) {
                now = DateTime.Now;
                double s = (now - birthday).TotalSeconds;
                int t = (int)(s * 10 % 10);
                Console.Write($"\r|{createCounter(t, 9)}|  あなたが生まれてから、{(int)s}秒");
            }

        }

        private static string createCounter(int cnt, int max) {
            StringBuilder sb = new StringBuilder();
            while (cnt > 0) {
                sb.Append("X");
                cnt--;
                max--;
            }
            while (max > 0) {
                sb.Append(" ");
                max--;
            }
            return sb.ToString();
        }
    }
}
