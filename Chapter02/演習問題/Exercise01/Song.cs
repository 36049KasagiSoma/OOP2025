using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    public class Song {
        //2.1.1
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public int Length { get; set; }

        //2.1.2
        public Song(string title, string artistName, int length) {
            Title = title;
            ArtistName = artistName;
            Length = length;
        }
    }
}
