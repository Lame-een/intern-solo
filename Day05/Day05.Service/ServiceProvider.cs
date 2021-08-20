using System;
using Day05.Repository;

namespace Day05.Service
{
    public class ServiceProvider
    {
        private static readonly ServiceProvider _instance = new ServiceProvider();

        private readonly SongRepository _songRepository = new SongRepository();
        private readonly SongService _songService;

        private readonly AlbumRepository _albumRepository = new AlbumRepository();
        private readonly AlbumService _albumService;

        static ServiceProvider()
        {
        }
        private ServiceProvider()
        {
            _songService = new SongService(_songRepository);
            _albumService = new AlbumService(_albumRepository);
        }
        public static ServiceProvider Instance
        {
            get
            {
                return _instance;
            }
        }

        public T Service<T>()
        {
            if (typeof(T) == typeof(SongService))
            {
                return (T)(object)_songService;
            }
            else if (typeof(T) == typeof(AlbumService))
            {
                return (T)(object)_albumService;
            }
            else
            {
                throw new TypeAccessException();
            }
        }
    }
}
