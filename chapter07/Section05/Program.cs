namespace Section05 {
    internal class Program {
        static void Main(string[] args) {
            var text = "The quick brown fox jumps over the lazy dog";
            var words = text.Split(" ");

            var word = words.FirstOrDefault(s => s.Length == 10);

            var numvers = new List<int>{ 9, 7, -5, -4, 2, 5, 4, 0, -4, 8, -1, 0, 4 };
            var index = numvers.FindIndex(n => n < 0);
            Console.WriteLine(index);
        }
    }
}
