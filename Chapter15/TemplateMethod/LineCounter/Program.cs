using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            string filePath;
            do {
                Console.Write("ファイルパス >");
                filePath = Console.ReadLine() ?? args[0];
            }while(!File.Exists(filePath));
            TextProcessor.Run<LineCounterProcessor>(filePath);
        }
    }
}
