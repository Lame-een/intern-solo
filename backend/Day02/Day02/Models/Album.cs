using System.Collections.Generic;

namespace Day02.Models
{
    public class Album
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public int NumberOfTracks { get; set; }

        public Album()
        {
        }
        public Album(string name, string artist, int tracknum)
        {
            Name = name;
            Artist = artist;
            NumberOfTracks = tracknum;
        }
    }
}