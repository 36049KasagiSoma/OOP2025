namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            List<GreetingBace> list = [
                new GreetingMorning(),
                new GreetingAfternoon(),
                new GreetingEvening(),
            ];

            foreach (var obj in list) {
                string msg = obj.GetMessage();
                Console.WriteLine(msg);
            }

        }

        class GreetingMorning:GreetingBace {
            public override string GetMessage() => "おはよう";
        }
        class GreetingAfternoon : GreetingBace {
            public override string GetMessage() => "こんにちは";
        }
        class GreetingEvening : GreetingBace {
            public override string GetMessage() => "こんばんは";
        }
    }
}
