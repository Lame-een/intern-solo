using System;
using System.Data.SqlClient;

namespace Day07.Repository
{
    public static class SqlUtilities
    {
        public static void AddParameterWithNullableValue(ref SqlCommand command, string parameterName, object value)
        {
            if (value == null)
            {
                command.Parameters.AddWithValue(parameterName, DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(parameterName, value);
            }
        }
    }
}
