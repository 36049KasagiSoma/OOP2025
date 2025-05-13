namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            var inputMusic = new InputSongController();

            inputMusic.StartInput();

            var songs = inputMusic.GetMySongs();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("**** 登録曲一覧 ***");

            printSongs(songs);
        }

        //2.1.4
        private static void printSongs(Song[] songs) {
            foreach (var song in songs) {
                var ts = TimeSpan.FromSeconds(song.Length);
                Console.WriteLine($"title:{song.Title,-30}artist:{song.ArtistName,-20}length:{ts.ToString(@"mm\:ss")}");
            }
        }

    }
}
