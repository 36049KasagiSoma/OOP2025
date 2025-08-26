using System.Text.RegularExpressions;

namespace Section04 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Word, Excel ,PowerPoint , Outlook,OneDrive";
            var pattern = @"\s*,\s*";
            var replaced = Regex.Replace(text, pattern, ", ");
            Console.WriteLine(replaced);
        }
    }
}
