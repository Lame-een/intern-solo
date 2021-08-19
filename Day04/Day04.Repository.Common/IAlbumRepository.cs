using System;
using System.Collections.Generic;
using Day04.Model.Common;

namespace Day04.Repository.Common
{
    public interface IAlbumRepository
    {
        IAlbum Select(Guid id);
        IAlbum Select(string name);
        List<IAlbum> SelectAll();
        List<IAlbum> SelectAll(Guid id);
        List<IAlbum> SelectAll(string name);

        int Insert(IAlbum value);
        int Update(Guid id, IAlbum value);
        int Update(string name, IAlbum value);
        int Delete(Guid id);
        int Delete(string name);
    }
}
