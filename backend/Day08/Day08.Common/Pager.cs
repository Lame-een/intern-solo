using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Day08.Common
{
    public class Pager
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public Pager(int pageSize = 10, int pageNumber = 1)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public Pager(int pageNumber = 1)
        {
            PageSize = 10;
            PageNumber = pageNumber;
        }

        public int Offset { get => (PageNumber - 1) * PageSize; }

        public string GetSql()
        {
            return " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY ";
        }

        public void AddParameters(ref SqlCommand command)
        {
            command.Parameters.AddWithValue("@Offset", Offset);
            command.Parameters.AddWithValue("@PageSize", PageSize);
        }

        
    }
}
