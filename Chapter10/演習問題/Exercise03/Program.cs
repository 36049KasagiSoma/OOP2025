namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            string path1 = string.Empty;
            do {
                Console.Write("path1:");
                path1 = Console.ReadLine() ?? string.Empty;
            } while (!File.Exists(path1));

            string path2 = string.Empty;
            do {
                Console.Write("path2:");
                path2 = Console.ReadLine() ?? string.Empty;
            } while (!File.Exists(path2));

            string outPath = string.Empty;
            Console.Write("out:");
            outPath = Console.ReadLine() ?? string.Empty;

            var lines1 = File.ReadAllLines(path1);
            var lines2 = File.ReadAllLines(path2);
            using (StreamWriter writer = new StreamWriter(outPath)) {
                foreach (var line in lines1) {
                    writer.WriteLine(line);
                }
                foreach (var line in lines2) {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
