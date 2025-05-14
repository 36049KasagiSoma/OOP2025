using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    /// <summary>
    /// 曲の入力をコントロールします。
    /// </summary>
    public class SongInputController {
        private List<Song> mySongs;

        public SongInputController() {
            mySongs = new List<Song>();
        }

        /// <summary>
        /// 曲の入力を開始します。
        /// </summary>
        public void StartInput() {
            Console.WriteLine("**** 曲の登録 *****");
            Console.WriteLine("曲名「end」で終了");
            Console.WriteLine();
            while (true) {
                Console.Write("曲名:");
                var songName = Console.ReadLine();
                if (String.IsNullOrEmpty(songName)) continue;
                if (songName.ToLower().Equals("end")) break;

                Console.Write("アーティスト名:");
                var artistName = Console.ReadLine();
                if (String.IsNullOrEmpty(artistName)) continue;

                Console.Write("長さ(秒):");
                var songLengthStr = Console.ReadLine();
                int songLength;
                if (String.IsNullOrEmpty(artistName) || !int.TryParse(songLengthStr, out songLength)) continue;

                mySongs.Add(new Song() {
                    Title = songName,
                    ArtistName = artistName,
                    Length = songLength
                });
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("*******************");
        }

        /// <summary>
        /// 入力された曲の配列を返します。
        /// </summary>
        /// <returns>入力された曲</returns>
        public Song[] GetMySongs() {
            return mySongs.ToArray();
        }

        /// <summary>
        /// 入力された曲を出力します。
        /// </summary>
        /// <param name="songs"></param>
        public void PrintSongs() {
            Song[] songs = GetMySongs();
            foreach (var song in songs) {
                var ts = TimeSpan.FromSeconds(song.Length);
                string title = PadRightMultibyte(song.Title, 30);
                string artist = PadRightMultibyte(song.ArtistName, 20);
                Console.WriteLine($"title:{title}artist:{artist}length:{ts.ToString(@"mm\:ss")}");
            }
        }

        //以下、調べた。
        private static string PadRightMultibyte(string input, int totalWidth) {
            int width = 0;
            foreach (char c in input) {
                width += IsWideChar(c) ? 2 : 1;
            }

            int padLength = totalWidth - width;
            if (padLength > 0) {
                return input + new string(' ', padLength);
            }
            return input;
        }

        private static bool IsWideChar(char c) {
            return c > 0xFF;
        }
    }
}
