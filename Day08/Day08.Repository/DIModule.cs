using Autofac;
using Day08.Repository.Common;

namespace Day08.Repository
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
            builder.RegisterType<SongRepository>().As<ISongRepository>();
        }
    }
}
