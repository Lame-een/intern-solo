using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day08.Model.Common;
using Day08.Model;
using Day08.Service.Common;
using Day08.Repository.Common;
using Day08.Common;

namespace Day08.Service
{
    public class SongService : ISongService
    {
        public SongService(ISongRepository repository)
        {
            Repository = repository;
        }
        protected ISongRepository Repository { get; private set; }
        public async Task<ISong> SelectAsync(Guid guid)
        {
            return await Repository.SelectAsync(guid);
        }

        public async Task<List<ISong>> SelectAsync(Pager pager, Sorter sorter, SongFilter filter)
        {
            return await Repository.SelectAsync(pager, sorter, filter);
        }


        public async Task<int> InsertAsync(ISong value)
        {
            return await Repository.InsertAsync(value);
        }

        public async Task<int> UpdateAsync<T>(T queryParameter, ISong value)
        {
            if (typeof(T) == typeof(Guid))
                return await Repository.UpdateAsync((Guid)(object)queryParameter, value);
            else if (typeof(T) == typeof(string))
                return await Repository.UpdateAsync((string)(object)queryParameter, value);
            else
                return -1;
        }

        public async Task<int> DeleteAsync<T>(T queryParameter)
        {
            if (typeof(T) == typeof(Guid))
                return await Repository.DeleteAsync((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return await Repository.DeleteAsync((string)(object)queryParameter);
            else
                return -1;
        }


        public ISong NewSong()
        {
            return new Song();
        }

        public ISong NewSong(Guid songGUID, string name, int? trackNum = null, Guid? albumGUID = null)
        {
            return new Song(songGUID, name, trackNum, albumGUID);
        }
        public ISong NewSong(string name, int? trackNum = null, Guid? albumGUID = null)
        {
            return new Song(name, trackNum, albumGUID);
        }
    }
}
