using System;
using System.IO;
using System.Data.SqlClient;
using Day03.Models;

namespace Day03.App_Code
{
    public sealed class DatabaseHandler
    {
        //illusion of privacy
        private const string _keyPath = "D:/key.key";
        private static string _connectionString = "";

        private static readonly DatabaseHandler _instance = new DatabaseHandler();

        private static void InitConnectionString()
        {
            FileStream fs = new FileStream(_keyPath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string aux = sr.ReadLine();
            _connectionString = $"Server = tcp:ajanjic-intern.database.windows.net,1433;Initial Catalog = mono-internship-db; Persist Security Info=False;User ID = ajanjic; Password={aux}; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
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

        #region Song CRUD

        public static int InsertSong(Song value)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            string sqlInsert = String.Format("INSERT INTO Song(Name, TrackNumber, AlbumID) " +
                "VALUES ('{0}', {1}, {2})",
                value.Name,
                value.TrackNumber == null ? "NULL" : value.TrackNumber.ToString(),
                value.AlbumGUID == null ? "NULL" : "'" + value.AlbumGUID.ToString() + "'");
            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    adapter.InsertCommand = new SqlCommand(sqlInsert, connection);
                    return adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public static int UpdateSong(Guid songID, Song value)
        {
            string sqlUpdate = String.Format("UPDATE song " +
                "SET Name = '{0}', TrackNumber = {1}, AlbumID = {2} " +
                "WHERE SongID = '{3}'",
                value.Name,
                value.TrackNumber == null ? "NULL" : value.TrackNumber.ToString(),
                value.AlbumGUID == null ? "NULL" : "'" + value.AlbumGUID.ToString() + "'",
                songID);

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    adapter.UpdateCommand = new SqlCommand(sqlUpdate, connection);
                    return adapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public static int DeleteSong(Guid songID)
        {
            string sqlDelete = String.Format("DELETE FROM song WHERE SongID = '{0}'", songID);

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    adapter.DeleteCommand = new SqlCommand(sqlDelete, connection);
                    return adapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
        #endregion

        #region Album CRUD

        public static int InsertAlbum(Album value)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            string sqlInsert = String.Format("INSERT INTO Album(Name, Artist, NumberOfTracks) " +
                "VALUES ('{0}', '{1}', {2})",
                value.Name,
                value.Artist,
                value.NumberOfTracks == null ? "NULL" : value.NumberOfTracks.ToString());
            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    adapter.InsertCommand = new SqlCommand(sqlInsert, connection);
                    return adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public static int UpdateAlbum(Guid albumID, Album value)
        {
            string sqlUpdate = String.Format("UPDATE album " +
                "SET Name = '{0}', Artist = '{1}', NumberOfTracks = {2} " +
                "WHERE AlbumID = '{3}'",
                value.Name,
                value.Artist,
                value.NumberOfTracks == null ? "NULL" : value.NumberOfTracks.ToString(),
                albumID);

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    adapter.UpdateCommand = new SqlCommand(sqlUpdate, connection);
                    return adapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public static int DeleteAlbum(Guid albumID)
        {
            string sqlDelete = String.Format("DELETE FROM album WHERE AlbumID = '{0}'", albumID);

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    adapter.DeleteCommand = new SqlCommand(sqlDelete, connection);
                    return adapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
        #endregion
    }
}