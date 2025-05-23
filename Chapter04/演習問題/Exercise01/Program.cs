
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
                "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {
            Console.WriteLine("= [foreach] ==========");
            foreach (var s in langs) {
                if (s.Contains('S')) Console.WriteLine(s);
            }
            Console.WriteLine("= [for] ==============");
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains('S')) Console.WriteLine(langs[i]);
            }
            Console.WriteLine("= [while] ============");
            int index = 0;
            while (index < langs.Count) {
                if (langs[index].Contains('S')) Console.WriteLine(langs[index]);
                index++;
            }
        }

        private static void Exercise2(List<string> langs) {
            langs.ForEach(s => {
                if (s.Contains('S')) Console.WriteLine(s);
            });
            //langs.Where(s => s.Contains('S')).ToList().ForEach(Console.WriteLine);
        }

        private static void Exercise3(List<string> langs) {
            var lang = langs.Find(s => s.Length == 10) ?? "unknown";
            Console.WriteLine(lang);
        }
    }
}
