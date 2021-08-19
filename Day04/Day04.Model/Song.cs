using System;
using Day04.Model.Common;

namespace Day04.Model
{
    public class Song : ISong
    {
        public const int FieldNumber = 4;
        public Guid SongID { get; set; }
        public string Name { get; set; }
        public int? TrackNumber { get; set; }
        public Guid? AlbumID { get; set; }

        public Song()
        {
            SongID = Guid.NewGuid();
        }
        public Song(Guid songGUID, string name, int? trackNum, Guid? albumGUID = null)
        {
            SongID = songGUID;
            Name = name;
            TrackNumber = trackNum;
            AlbumID = albumGUID;
        }
        public Song(string name, int? trackNum, Guid? albumGUID = null)
        {
            SongID = Guid.NewGuid();
            Name = name;
            TrackNumber = trackNum;
            AlbumID = albumGUID;
        }

        public Song(object[] columns)
        {
            SongID = (Guid)columns[0];
            Name = (string)columns[1];
            TrackNumber = columns[2] as int?;
            AlbumID = columns[3] as Guid?;
        }
    }
}