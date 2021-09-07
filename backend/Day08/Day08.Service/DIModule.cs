using Autofac;
using Day08.Service.Common;

namespace Day08.Service
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlbumService>().As<IAlbumService>();
            builder.RegisterType<SongService>().As<ISongService>();
        }
    }
}
