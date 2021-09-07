using System;
using System.Collections.Generic;
using Day04.Repository.Common;
using Day04.Model;
using Day04.Model.Common;

namespace Day04.Service
{
    public class AlbumService
    {
        public AlbumService(IAlbumRepository repository)
        {
            Repository = repository;
        }
        protected IAlbumRepository Repository { get; private set; }
        public IAlbum Select<T>(T queryParameter)
        {
            //extreeeeemely ugly - mistakes were made
            if (typeof(T) == typeof(Guid))
                return Repository.Select((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return Repository.Select((string)(object)queryParameter);
            else
                return null;
        }

        public List<IAlbum> SelectAll()
        {
            return Repository.SelectAll();
        }

        public List<IAlbum> SelectAll<T>(T queryParameter)
        {
            if (typeof(T) == typeof(Guid))
                return Repository.SelectAll((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return Repository.SelectAll((string)(object)queryParameter);
            else
                return null;
        }

        public int Insert(IAlbum value)
        {
            return Repository.Insert(value);
        }

        public int Insert(RESTAlbum value)
        {
            if (value.Name == null)
                return -1;

            if (value.AlbumID.HasValue)
            {
                IAlbum temp = new Album((Guid)value.AlbumID, value.Name, value.Artist, value.NumberOfTracks);
                return Insert(temp);
            }
            else
            {
                IAlbum temp = new Album(value.Name, value.Artist, value.NumberOfTracks);
                return Insert(temp);
            }
        }

        public int Put(RESTAlbum value)
        {
            if (value.Name == null)
                return -1;

            if (value.AlbumID.HasValue)
            {
                IAlbum temp = new Album((Guid)value.AlbumID, value.Name, value.Artist, value.NumberOfTracks);
                return Update(value.AlbumID, temp);
            }
            else
            {
                return Insert(value);
            }
        }

        public int Update<T>(T queryParameter, IAlbum value)
        {
            if (typeof(T) == typeof(Guid))
                return Repository.Update((Guid)(object)queryParameter, value);
            else if (typeof(T) == typeof(string))
                return Repository.Update((string)(object)queryParameter, value);
            else
                return -1;
        }

        public int Delete<T>(T queryParameter)
        {
            if (typeof(T) == typeof(Guid))
                return Repository.Delete((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return Repository.Delete((string)(object)queryParameter);
            else
                return -1;
        }
    }
}
