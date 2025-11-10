namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            CountClass cc = new CountClass("source.txt");
            int cnt1 = cc.CountClassStr();
            int cnt2 = cc.CountClassStr2();
            int cnt3 = cc.CountClassStr3();


            Console.WriteLine($"10.1 classの数:{cnt1}");
            Console.WriteLine($"10.2 classの数:{cnt2}");
            Console.WriteLine($"10.3 classの数:{cnt3}");
        }
    }
}
