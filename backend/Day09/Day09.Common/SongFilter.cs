using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Day09.Common
{
    public class SongFilter
    {
        public string FilterString { get; set; }

        public SongFilter(string filter = null)
        {
            FilterString = filter;
        }

        public string GetSql()
        {
            if (FilterString == null) return "";


            bool isInt = int.TryParse(FilterString, out int _);

            string ret = " WHERE (Name LIKE @QueryString) " + (isInt ? " OR TrackNumber = @QueryInt " : "");

            return ret;
        }

        public void AddParameters(ref SqlCommand command)
        {
            if (FilterString == null) return;

            if (int.TryParse(FilterString, out int  _)){
                command.Parameters.AddWithValue("@QueryInt", int.Parse(FilterString));
            }

            command.Parameters.AddWithValue("@QueryString", '%' + FilterString + '%');
        }
    }
}
