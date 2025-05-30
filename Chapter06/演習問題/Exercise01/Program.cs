using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var target = "C# Programming";
            var isExist = target.All(Char.IsLower);
            Console.WriteLine(isExist);
        }
    }
}
