using TextFileProcessor;

namespace Exercise01 {

    internal class Program {
        static void Main(string[] args) {
            TextProcessor.Run<MyTextProcessor>("test.txt");
        }
    }
}
