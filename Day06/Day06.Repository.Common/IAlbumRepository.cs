using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day06.Model.Common;

namespace Day06.Repository.Common
{
    public interface IAlbumRepository
    {
        Task<IAlbum> SelectAsync(Guid id);
        Task<IAlbum> SelectAsync(string name);
        Task<List<IAlbum>> SelectAllAsync();
        Task<List<IAlbum>> SelectAllAsync(string name);

        Task<int> InsertAsync(IAlbum value);
        Task<int> UpdateAsync(Guid id, IAlbum value);
        Task<int> UpdateAsync(string name, IAlbum value);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(string name);
    }
}
