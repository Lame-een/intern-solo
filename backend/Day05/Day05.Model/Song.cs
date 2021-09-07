using System;
using Day05.Model.Common;
using Day05.Global;

namespace Day05.Model
{
    public class Song : ISong
    {
        public const int FieldNumber = 7;
        public Guid SongID { get; set; }
        public string Name { get; set; }
        public int? TrackNumber { get; set; }
        public Guid? AlbumID { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CreatedBy { get; set; }

        public Song()
        {
            SongID = Guid.NewGuid();
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }
        public Song(Guid songGUID, string name, int? trackNum = null, Guid? albumGUID = null)
        {
            SongID = songGUID;
            Name = name;
            TrackNumber = trackNum;
            AlbumID = albumGUID;
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }
        public Song(string name, int? trackNum = null, Guid? albumGUID = null)
        {
            SongID = Guid.NewGuid();
            Name = name;
            TrackNumber = trackNum;
            AlbumID = albumGUID;
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }

        public Song(object[] columns)
        {
            SongID = (Guid)columns[0];
            Name = (string)columns[1];
            TrackNumber = columns[2] as int?;
            AlbumID = columns[3] as Guid?;
            DateCreated = DateTime.Now;
            CreatedBy = Globals.CurrentUser;
        }
    }
}