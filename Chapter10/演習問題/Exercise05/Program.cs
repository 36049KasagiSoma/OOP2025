namespace Exercise05 {
    internal class Program {
        static void Main(string[] args) {
            string path1 = string.Empty;
            do {
                Console.Write("path1:");
                path1 = Console.ReadLine() ?? string.Empty;
            } while (!Directory.Exists(path1));

            string path2 = string.Empty;
            Console.Write("backupDir:");
            path2 = Console.ReadLine() ?? string.Empty;

            if (!Directory.Exists(path2)) {
                Directory.CreateDirectory(path2);
            }

            List<string> files = GetAllDirectories(path1);
            foreach (var file in files) {
                string tmp = Path.GetFileName(file);
                string name = tmp.Substring(0, tmp.LastIndexOf('.')) + "_bak"
                    + tmp.Substring(tmp.LastIndexOf('.'));
                File.Copy(file, Path.Combine(path2, name));
            }
        }

        static List<string> GetAllDirectories(string path) {
            List<string> filse = new List<string>();
            foreach (var file in Directory.GetFiles(path)) {
                if (File.Exists(file))
                    filse.Add(file);
            }
            return filse;
        }
    }
}
