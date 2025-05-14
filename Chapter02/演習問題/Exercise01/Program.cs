namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            var inputMusic = new SongInputController();

            inputMusic.StartInput();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("**** 登録曲一覧 ***");

            inputMusic.PrintSongs();
        }

    }
}
