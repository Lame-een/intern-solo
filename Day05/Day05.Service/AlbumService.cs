using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day05.Repository.Common;
using Day05.Model;
using Day05.Model.Common;
using Day05.Service.Common;

namespace Day05.Service
{
    public class AlbumService : IAlbumService
    {
        public AlbumService(IAlbumRepository repository)
        {
            Repository = repository;
        }
        protected IAlbumRepository Repository { get; private set; }
        public async Task<IAlbum> SelectAsync<T>(T queryParameter)
        {
            //extreeeeemely ugly - mistakes were made
            if (typeof(T) == typeof(Guid))
                return await Repository.SelectAsync((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return await Repository.SelectAsync((string)(object)queryParameter);
            else
                return null;
        }

        public async Task<List<IAlbum>> SelectAllAsync()
        {
            return await Repository.SelectAllAsync();
        }

        public async Task<List<IAlbum>> SelectAllAsync(string queryParameter)
        {
            return await Repository.SelectAllAsync(queryParameter);
        }

        public async Task<int> InsertAsync(IAlbum value)
        {
            return await Repository.InsertAsync(value);
        }

        public async Task<int> UpdateAsync<T>(T queryParameter, IAlbum value)
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

        public IAlbum NewAlbum()
        {
            return new Album();
        }
        public IAlbum NewAlbum(Guid albumGUID, string name, string artist, int? trackNum = null)
        {
            return new Album(albumGUID, name, artist, trackNum);
        }
        public IAlbum NewAlbum(string name, string artist, int? trackNum = null)
        {
            return new Album(name, artist, trackNum);
        }
    }
}
