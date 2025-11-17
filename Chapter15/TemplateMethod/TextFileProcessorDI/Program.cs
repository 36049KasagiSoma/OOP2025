namespace TextFileProcessorDI {
    internal class Program {
        static void Main(string[] args) {
            var service = new LineOutputService(20);
            TextFileProcessor processor = new TextFileProcessor(service);
            
            string? inputPath = null;
            Console.Write("パスを入力:");
            
            do {
                inputPath = Console.ReadLine();
            } while (inputPath is null || !File.Exists(inputPath));
            
            processor.Run(inputPath);

        }
    }
}
