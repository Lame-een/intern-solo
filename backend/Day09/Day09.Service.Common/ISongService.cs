using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day09.Model.Common;
using Day09.Common;

namespace Day09.Service.Common
{
    public interface ISongService
    {
        Task<List<ISong>> SelectAsync(Pager pager, Sorter sorter, SongFilter filter);
        Task<ISong> SelectAsync(Guid guid);

        Task<int> InsertAsync(ISong value);
        Task<int> UpdateAsync<T>(T queryParameter, ISong value);
        Task<int> DeleteAsync<T>(T queryParameter);


        ISong NewSong();

        ISong NewSong(Guid songGUID, string name, int? trackNum = null, Guid? albumGUID = null);
        ISong NewSong(string name, int? trackNum = null, Guid? albumGUID = null);
    }
}
