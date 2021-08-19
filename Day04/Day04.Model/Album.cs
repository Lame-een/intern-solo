using System;
using Day04.Model.Common;

namespace Day04.Model
{
    public class Album : IAlbum
    {
        public const int FieldNumber = 4;
        public Guid AlbumID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int? NumberOfTracks { get; set; }

        public Album()
        {
        }

        public Album(Guid albumGUID, string name, string artist, int? trackNum)
        {
            AlbumID = albumGUID;
            Name = name;
            Artist = artist;
            NumberOfTracks = trackNum;
        }
        public Album(string name, string artist, int? trackNum)
        {
            AlbumID = Guid.NewGuid();
            Name = name;
            Artist = artist;
            NumberOfTracks = trackNum;
        }

        public Album(object[] columns)
        {
            AlbumID = (Guid)columns[0];
            Name = (string)columns[1];
            Artist = (string)columns[2];
            NumberOfTracks = columns[3] as int?;
        }
    }
}