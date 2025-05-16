namespace Section01 {
    internal class Program {

        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo",
                "New Delhi",
                "Bangkok",
                "London",
                "Paris",
                "Berlin",
                "Canberra",
                "Hong Kong",
            };

            var lowerList = cities.ConvertAll(s => s.ToLower());
            lowerList.ForEach(Console.WriteLine);

            Console.WriteLine();
            Console.WriteLine();

            var upperList = cities.ConvertAll(s => s.ToUpper());
            upperList.ForEach(Console.WriteLine);

        }


    }
}
