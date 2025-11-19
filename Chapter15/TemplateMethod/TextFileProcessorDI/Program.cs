namespace TextFileProcessorDI {
    internal class Program {
        static void Main(string[] args) {
            var service = new LineToHalfNumberService();
            TextFileProcessor processor = new TextFileProcessor(service);
            
            string? inputPath = null;

            do {
                Console.Write("パスを入力:");
                inputPath = Console.ReadLine();
            } while (inputPath is null || !File.Exists(inputPath));
            
            processor.Run(inputPath);

        }
    }
}
