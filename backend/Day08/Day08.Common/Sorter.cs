using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Day08.Common
{
    public class Sorter
    {
        public enum OrderType : byte
        {
            ASC = 0,
            DESC = 1
        }
        public OrderType Order { get; set; }
        public string SortBy { get; set; }

        public string GetSql(Type type)
        {
            if (SortBy == null)
                return String.Format(" ORDER BY (SELECT NULL) {0} ", Order.ToString());

            var names = (from name in type.GetProperties() select name.Name.ToLower());

            if (names.Contains(SortBy.ToLower()))
            {
                return String.Format(" ORDER BY {0} {1}", SortBy, Order.ToString());
            }
            else
            {
                return String.Format(" ORDER BY (SELECT NULL) {0} ", Order.ToString());
            }
        }

        public Sorter(string sortBy, OrderType order = OrderType.ASC)
        {
            SortBy = sortBy;
            Order = order;
        }

        public Sorter(OrderType order = OrderType.ASC)
        {
            SortBy = null;
            Order = order;
        }

    }
}