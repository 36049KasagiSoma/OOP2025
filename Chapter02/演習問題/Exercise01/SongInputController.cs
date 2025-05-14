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
                if (songName.Equals("end")) break;

                Console.Write("アーティスト名:");
                var artistName = Console.ReadLine();
                if (String.IsNullOrEmpty(artistName)) continue;

                Console.Write("長さ(秒):");
                var songLengthStr = Console.ReadLine();
                int songLength;
                if (String.IsNullOrEmpty(artistName) || !int.TryParse(songLengthStr, out songLength)) continue;

                mySongs.Add(new Song(songName, artistName, songLength));
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
    }
}
