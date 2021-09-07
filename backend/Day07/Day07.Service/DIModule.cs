using Autofac;
using Day07.Service.Common;

namespace Day07.Service
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
