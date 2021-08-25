using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day08.Model.Common;
using Day08.Common;

namespace Day08.Repository.Common
{
    public interface IAlbumRepository
    {
        Task<IAlbum> SelectAsync(Guid id);
        Task<List<IAlbum>> SelectAsync(Pager pager, Sorter sorter, AlbumFilter filter);

        Task<int> InsertAsync(IAlbum value);
        Task<int> UpdateAsync(Guid id, IAlbum value);
        Task<int> UpdateAsync(string name, IAlbum value);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(string name);
    }
}
