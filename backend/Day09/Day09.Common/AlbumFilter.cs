using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09.Common
{
    public class AlbumFilter
    {
        public string FilterString { get; set; }

        public AlbumFilter(string filter = null)
        {
            FilterString = filter;
        }

        public string GetSql()
        {
            if (FilterString == null) return "";


            bool isInt = int.TryParse(FilterString, out int _);

            string ret = " WHERE (Name LIKE @QueryString) OR (Artist LIKE @QueryString) " + (isInt ? " OR NumberOfTracks = @QueryInt " : "");

            return ret;
        }

        public void AddParameters(ref SqlCommand command)
        {
            if (FilterString == null) return;

            if (int.TryParse(FilterString, out int _))
            {
                command.Parameters.AddWithValue("@QueryInt", int.Parse(FilterString));
            }

            command.Parameters.AddWithValue("@QueryString", '%' + FilterString + '%');
        }
    }
}
