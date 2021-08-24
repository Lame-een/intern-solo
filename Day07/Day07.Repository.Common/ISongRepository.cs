using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day07.Model.Common;

namespace Day07.Repository.Common
{
    public interface ISongRepository
    {
        Task<ISong> SelectAsync(Guid id);
        Task<ISong> SelectAsync(string name);
        Task<List<ISong>> SelectAllAsync();
        Task<List<ISong>> SelectAllAsync(string name);

        Task<int> InsertAsync(ISong value);
        Task<int> UpdateAsync(Guid id, ISong value);
        Task<int> UpdateAsync(string name, ISong value);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(string name);
    }
}
