using System;
namespace Day06.Model.Common
{
    public interface ISong
    {
        Guid SongID { get; set; }
        string Name { get; set; }
        int? TrackNumber { get; set; }
        Guid? AlbumID { get; set; }
        DateTime? DateCreated { get; set; }
        Guid? CreatedBy { get; set; }
    }
}
