using System;

namespace Day04.Model
{
    public class RESTSong
    {
        public const int FieldNumber = 4;
        public Guid? SongID { get; set; }
        public string Name { get; set; }
        public int? TrackNumber { get; set; }
        public Guid? AlbumID { get; set; }
    }
}