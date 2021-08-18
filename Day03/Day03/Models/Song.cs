using System;

namespace Day03.Models
{
    public class Song
    {
        public Guid SongGUID { get; set; }
        public string Name { get; set; }
        public int? TrackNumber { get; set; }

        //foreign key - can be null
        public Guid? AlbumGUID { get; set; }

        public Song()
        {
            SongGUID = Guid.NewGuid();
        }
        public Song(Guid songGUID, string name, int? trackNum, Guid? albumGUID = null)
        {
            SongGUID = songGUID;
            Name = name;
            TrackNumber = trackNum;
            AlbumGUID = albumGUID;
        }
        public Song(string name, int? trackNum, Guid? albumGUID = null)
        {
            Name = name;
            TrackNumber = trackNum;
            AlbumGUID = albumGUID;
        }

        public Song(object[] columns)
        {
            SongGUID = (Guid)columns[0];
            Name = (string)columns[1];
            TrackNumber = columns[2] as int?;
            AlbumGUID = columns[3] as Guid?;
        }
    }
}