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

        }

        static bool IsPhoneNumber(string s) {
            return Regex.IsMatch(s, @"^(090|080|070)-\d{4}-\d{4}$");
        }
    }
}
