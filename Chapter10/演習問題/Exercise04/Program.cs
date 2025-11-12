namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            string path = string.Empty;
            const long megabyte = 1048576;
            do {
                Console.Write("path1:");
                path = Console.ReadLine() ?? string.Empty;
            } while (!Directory.Exists(path));

            List<string> files = GetAllFiles(path);
            foreach (string file in files) {
                var size = new FileInfo(file).Length;
                if (size >= megabyte) {
                    string name = Path.GetFileName(file);
                    Console.WriteLine($"{size/megabyte,5} MB({size,10} B):{name}");
                }

            }
        }

        static List<string> GetAllFiles(string path) {
            List<string> files = new List<string>();
            foreach (var file in Directory.GetFiles(path)) {
                if(File.Exists(file))
                    files.Add(file);
            }
            foreach (var dir in Directory.GetDirectories(path)) {
                if(Directory.Exists(dir))
                    files.AddRange(GetAllFiles(dir));
            }
            return files;
        }
    }
}
