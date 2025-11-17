namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var from = GetConverter("変換元の単位を入力してください");
            var to = GetConverter("変換先の単位を入力してください");
            var distance = GetDistance(from);

            var converter = new DistanceConverter(from, to);
            var result = converter.Convert(distance);
            Console.WriteLine($"{distance}{from.UnitName}は、{result}{to.UnitName}です");
        }

        static double GetDistance(ConverterBase from) {
            double? value = null;
            do {
                Console.Write($"変換したい距離(単位:{from.UnitName})を入力してください => ");
                var line = Console.ReadLine();
                value = double.TryParse(line, out var temp) ? temp : null;
            } while (value is null);
            return value.Value;
        }

        static ConverterBase GetConverter(string msg) {
            ConverterBase? converter = null;
            do {
                Console.Write(msg + " => ");
                var unit = Console.ReadLine();
                if (unit != null)
                    converter = ConverterFactory.GetInstance(unit);
            } while (converter is null);
            return converter;
        }

    }
}
