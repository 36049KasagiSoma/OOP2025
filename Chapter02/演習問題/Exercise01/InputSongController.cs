using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    public class InputSongController {
        private List<Song> mySongs;
        public InputSongController() {
            mySongs = new List<Song>();
        }

        public void StartInput() {
            Console.WriteLine("**** 曲の登録 *****");

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

        public Song[] GetMySongs() {
            return mySongs.ToArray();
        }
    }
}
