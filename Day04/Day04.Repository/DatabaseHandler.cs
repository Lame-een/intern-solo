using System;
using System.IO;
using System.Data.SqlClient;

namespace Day04.Repository
{
    public sealed class DatabaseHandler
    {
        //illusion of privacy
        private const string _webConfigPath = "D:/dbcfg.cfg";
        private static string _connectionString = "";

        private static readonly DatabaseHandler _instance = new DatabaseHandler();

        private static void InitConnectionString()
        {
            //can be handled better, too lazy
            using (FileStream fs = new FileStream(_webConfigPath, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                _connectionString = sr.ReadLine();
            }
        }

        static DatabaseHandler()
        {
        }
        private DatabaseHandler()
        {
            InitConnectionString();
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