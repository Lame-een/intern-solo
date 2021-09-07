using System;
namespace Day04.Model.Common
{
    public interface ISong
    {
        Guid SongID { get; set; }
        string Name { get; set; }
        int? TrackNumber { get; set; }
        Guid? AlbumID { get; set; }
    }
}
