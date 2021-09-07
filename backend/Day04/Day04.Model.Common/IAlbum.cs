using System;

namespace Day04.Model.Common
{
    public interface IAlbum
    {
        Guid AlbumID { get; set; }
        string Name { get; set; }
        string Artist { get; set; }
        int? NumberOfTracks { get; set; }
    }
}
