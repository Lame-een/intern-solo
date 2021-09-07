using System;
using System.Collections.Generic;
using Day04.Model.Common;

namespace Day04.Service.Common
{
    public interface IAlbumService
    {
        IAlbum Select<T>(T queryParameter);
        List<IAlbum> SelectAll();
        List<IAlbum> SelectAll<T>(T queryParameter);

        int Insert(IAlbum value);
        int Update<T>(T queryParameter, IAlbum value);
        int Delete<T>(T queryParameter);
    }
}
