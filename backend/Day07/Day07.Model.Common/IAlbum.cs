using System;

namespace Day07.Model.Common
{
    public interface IAlbum
    {
        Guid AlbumID { get; set; }
        string Name { get; set; }
        string Artist { get; set; }
        int? NumberOfTracks { get; set; }

        DateTime? DateCreated { get; set; }
        Guid? CreatedBy { get; set; }
    }
}
