using System;
using System.Collections.Generic;
using Day04.Model.Common;

namespace Day04.Service.Common
{
    public interface ISongService
    {
        ISong Select<T>(T queryParameter);
        List<ISong> SelectAll();
        List<ISong> SelectAll<T>(T queryParameter);

        int Insert(ISong value);
        int Update<T>(T queryParameter, ISong value);
        int Delete<T>(T queryParameter);
    }
}
