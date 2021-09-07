using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day09.Model.Common;
using Day09.Common;

namespace Day09.Repository.Common
{
    public interface ISongRepository
    {
        Task<ISong> SelectAsync(Guid id);
        Task<List<ISong>> SelectAsync(Pager pager, Sorter sorter, SongFilter filter);

        Task<int> InsertAsync(ISong value);
        Task<int> UpdateAsync(Guid id, ISong value);
        Task<int> UpdateAsync(string name, ISong value);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(string name);
    }
}
