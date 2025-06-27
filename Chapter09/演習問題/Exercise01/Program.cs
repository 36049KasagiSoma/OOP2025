using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var dateTime = DateTime.Now;

            DisplayPattern1(dateTime);
            DisplayPattern2(dateTime);
            DisplayPattern3(dateTime);
        }

        private static void DisplayPattern1(DateTime dateTime) {
            Console.WriteLine(string.Format("{0:yyyy/MM/dd H:mm}", dateTime));
        }

        private static void DisplayPattern2(DateTime dateTime) {
            Console.WriteLine(dateTime.ToString("yyyy年MM月dd日 H時mm分ss秒"));
        }

        private static void DisplayPattern3(DateTime dateTime) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            Console.WriteLine(
                dateTime.ToString($"gg y年 M月 d日（{culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek)}）"
                , culture)
            );
        }
    }
}
