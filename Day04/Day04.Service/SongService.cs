using System;
using System.Collections.Generic;
using Day04.Model.Common;
using Day04.Model;
using Day04.Service.Common;
using Day04.Repository.Common;

namespace Day04.Service
{
    public class SongService : ISongService
    {
        public SongService(ISongRepository repository)
        {
            Repository = repository;
        }
        protected ISongRepository Repository { get; private set; }
        public ISong Select<T>(T queryParameter)
        {
            //extreeeeemely ugly - mistakes were made
            if (typeof(T) == typeof(Guid))
                return Repository.Select((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return Repository.Select((string)(object)queryParameter);
            else
                return null;
        }

        public List<ISong> SelectAll()
        {
            return Repository.SelectAll();
        }

        public List<ISong> SelectAll<T>(T queryParameter)
        {
            if (typeof(T) == typeof(Guid))
                return Repository.SelectAll((Guid)(object)queryParameter);
            else if (typeof(T) == typeof(string))
                return Repository.SelectAll((string)(object)queryParameter);
            else
                return null;
        }


        public int Insert(ISong value)
        {
            return Repository.Insert(value);
        }

        public int Insert(RESTSong value)
        {
            if (value.Name == null)
                return -1;

            if (value.SongID.HasValue)
            {
                ISong temp = new Song((Guid)value.SongID, value.Name, value.TrackNumber, value.AlbumID);
                return Insert(temp);
            }
            else
            {
                ISong temp = new Song(value.Name, value.TrackNumber, value.AlbumID);
                return Insert(temp);
            }
        }

        public int Put(RESTSong value)
        {
            if (value.Name == null)
                return -1;

            if (value.SongID.HasValue)
            {
                ISong temp = new Song((Guid)value.SongID, value.Name, value.TrackNumber, value.AlbumID);
                return Update(temp.SongID, temp);
            }
            else
            {
                return Insert(value);
            }
        }
        public int Update<T>(T queryParameter, ISong value)
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
