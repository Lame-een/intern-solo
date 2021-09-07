using Autofac;
using Day07.Repository.Common;

namespace Day07.Repository
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
