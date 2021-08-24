using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day07.Model.Common;
using Day07.Model;
using Day07.Service.Common;
using Day07.Repository.Common;

namespace Day07.Service
{
    public class SongService : ISongService
    {
        public SongService(ISongRepository repository)
        {
            Repository = repository;
        }
        protected ISongRepository Repository { get; private set; }
        public async Task<ISong> SelectAsync<T>(T queryParameter)
        {
            //extreeeeemely ugly - mistakes were made
            if (typeof(T) == typeof(Guid))
                return await Repository.SelectAsync((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return await Repository.SelectAsync((string)(object)queryParameter);
            else
                return null;
        }

        public async Task<List<ISong>> SelectAllAsync()
        {
            return await Repository.SelectAllAsync();
        }

        public async Task<List<ISong>> SelectAllAsync(string queryParameter)
        {
            return await Repository.SelectAllAsync(queryParameter);
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
