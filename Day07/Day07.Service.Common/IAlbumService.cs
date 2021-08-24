using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day07.Model.Common;

namespace Day07.Service.Common
{
    public interface IAlbumService
    {
        Task<IAlbum> SelectAsync<T>(T queryParameter);
        Task<List<IAlbum>> SelectAllAsync();
        Task<List<IAlbum>> SelectAllAsync(string queryParameter);

        Task<int> InsertAsync(IAlbum value);
        Task<int> UpdateAsync<T>(T queryParameter, IAlbum value);
        Task<int> DeleteAsync<T>(T queryParameter);

        IAlbum NewAlbum();
        IAlbum NewAlbum(Guid albumGUID, string name, string artist, int? trackNum = null);
        IAlbum NewAlbum(string name, string artist, int? trackNum = null);
    }
}
