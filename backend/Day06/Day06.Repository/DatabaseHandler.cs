using System;
using System.IO;
using System.Data.SqlClient;

namespace Day06.Repository
{
    //should be thread safe
    public sealed class DatabaseHandler
    {
        private const string _connectionString = "Server=tcp:ajanjic-intern.database.windows.net,1433;Initial Catalog=mono-internship-db;Persist Security Info=False;User ID=ajanjic;Password=IZwLQysyaSXmxg3QNpeO;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static readonly DatabaseHandler _instance = new DatabaseHandler();

        static DatabaseHandler()
        {
        }
        private DatabaseHandler()
        {
        }
        public static DatabaseHandler Instance
        {
            get
            {
                return _instance;
            }
        }

        public SqlConnection NewConnection()
        {
            return new SqlConnection(_connectionString);
        }

    }
}