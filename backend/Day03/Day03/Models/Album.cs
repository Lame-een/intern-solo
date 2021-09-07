using System;

namespace Day03.Models
{
    public class Album
    {
        public Guid AlbumGUID { get; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int? NumberOfTracks { get; set; }

        public Album()
        {
            AlbumGUID = Guid.NewGuid();
        }

        public Album(Guid albumGUID, string name, string artist, int? trackNum)
        {
            AlbumGUID = albumGUID;
            Name = name;
            Artist = artist;
            NumberOfTracks = trackNum;
        }
        public Album(string name, string artist, int? trackNum)
        {
            Name = name;
            Artist = artist;
            NumberOfTracks = trackNum;
        }

        public Album(object[] columns)
        {
            AlbumGUID = (Guid)columns[0];
            Name = (string)columns[1];
            Artist = (string)columns[2];
            NumberOfTracks = columns[3] as int?;
        }
    }
}