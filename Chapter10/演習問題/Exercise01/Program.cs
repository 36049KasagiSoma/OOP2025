namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            int cnt = new CountClass("source.txt").CountClassStr();
            Console.WriteLine($"classの数:{cnt}");
        }
    }
}
