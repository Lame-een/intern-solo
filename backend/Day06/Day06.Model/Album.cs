using System;
using Day06.Model.Common;
using Day06.Global;

namespace Day06.Model
{
    public class Album : IAlbum
    {
        public const int FieldNumber = 7;
        public Guid AlbumID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int? NumberOfTracks { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public Album()
        {
            AlbumID = Guid.NewGuid();
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }

        public Album(Guid albumGUID, string name, string artist, int? trackNum)
        {
            AlbumID = albumGUID;
            Name = name;
            Artist = artist;
            NumberOfTracks = trackNum;
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }
        public Album(string name, string artist, int? trackNum)
        {
            AlbumID = Guid.NewGuid();
            Name = name;
            Artist = artist;
            NumberOfTracks = trackNum;
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }

        public Album(object[] columns)
        {
            AlbumID = (Guid)columns[0];
            Name = (string)columns[1];
            Artist = (string)columns[2];
            NumberOfTracks = columns[3] as int?;
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }
    }
}