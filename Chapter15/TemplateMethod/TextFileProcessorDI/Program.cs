namespace TextFileProcessorDI {
    internal class Program {
        static void Main(string[] args) {
            var service = new LineCounterService();
            TextFileProcessor processor = new TextFileProcessor(service);
            Console.Write("パスを入力:");
            processor.Run(Console.ReadLine() ?? "sample.txt");
        }
    }
}
