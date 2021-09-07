using System;
using System.Collections.Generic;
using Day04.Model.Common;

namespace Day04.Repository.Common
{
    public interface ISongRepository
    {
        ISong Select(Guid id);
        ISong Select(string name);
        List<ISong> SelectAll();
        List<ISong> SelectAll(Guid id);
        List<ISong> SelectAll(string name);

        int Insert(ISong value);
        int Update(Guid id, ISong value);
        int Update(string name, ISong value);
        int Delete(Guid id);
        int Delete(string name);
    }
}
