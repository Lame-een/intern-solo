using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day06.Model.Common;

namespace Day06.Service.Common
{
    public interface ISongService
    {
        Task<ISong> SelectAsync<T>(T queryParameter);
        Task<List<ISong>> SelectAllAsync();
        Task<List<ISong>> SelectAllAsync(string queryParameter);

        Task<int> InsertAsync(ISong value);
        Task<int> UpdateAsync<T>(T queryParameter, ISong value);
        Task<int> DeleteAsync<T>(T queryParameter);


        ISong NewSong();

        ISong NewSong(Guid songGUID, string name, int? trackNum = null, Guid? albumGUID = null);
        ISong NewSong(string name, int? trackNum = null, Guid? albumGUID = null);
    }
}
