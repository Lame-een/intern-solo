using Autofac;
using Day09.Service.Common;

namespace Day09.Service
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
