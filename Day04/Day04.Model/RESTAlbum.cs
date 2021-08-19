using System;

namespace Day04.Model
{
    public class RESTAlbum
    {
        public const int FieldNumber = 4;
        public Guid? AlbumID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int? NumberOfTracks { get; set; }
    }
}