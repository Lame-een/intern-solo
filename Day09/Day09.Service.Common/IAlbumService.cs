using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day09.Model.Common;
using Day09.Common;

namespace Day09.Service.Common
{
    public interface IAlbumService
    {
        Task<IAlbum> SelectAsync(Guid id);
        Task<List<IAlbum>> SelectAsync(Pager pager, Sorter sorter, AlbumFilter filter);

        Task<int> InsertAsync(IAlbum value);
        Task<int> UpdateAsync<T>(T queryParameter, IAlbum value);
        Task<int> DeleteAsync<T>(T queryParameter);

        IAlbum NewAlbum();
        IAlbum NewAlbum(Guid albumGUID, string name, string artist, int? trackNum = null);
        IAlbum NewAlbum(string name, string artist, int? trackNum = null);
    }
}
