using System;
using System.Data;
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

            const string sqlInsert = "INSERT INTO Song(Name, TrackNumber, AlbumID) VALUES (@Name, @TrackNumber, @AlbumID)";
            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlInsert, connection);
                    command.Parameters.AddWithValue("@Name", value.Name);

                    if (value.TrackNumber.HasValue)
                        command.Parameters.AddWithValue("@TrackNumber", value.TrackNumber);
                    else
                        command.Parameters.AddWithValue("@TrackNumber", DBNull.Value);

                    if (value.AlbumGUID.HasValue)
                        command.Parameters.AddWithValue("@AlbumID", value.AlbumGUID);
                    else
                        command.Parameters.AddWithValue("@AlbumID", DBNull.Value);

                    adapter.InsertCommand = command;
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
            string sqlUpdate = "UPDATE song SET Name = @Name, TrackNumber = @TrackNumber, AlbumID = @AlbumID WHERE SongID = @SongID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlUpdate, connection);
                    command.Parameters.AddWithValue("@Name", value.Name);

                    if (value.TrackNumber.HasValue)
                        command.Parameters.AddWithValue("@TrackNumber", value.TrackNumber);
                    else
                        command.Parameters.AddWithValue("@TrackNumber", DBNull.Value);

                    if (value.AlbumGUID.HasValue)
                        command.Parameters.AddWithValue("@AlbumID", value.AlbumGUID);
                    else
                        command.Parameters.AddWithValue("@AlbumID", DBNull.Value);

                    command.Parameters.AddWithValue("@SongID", songID);

                    adapter.UpdateCommand = command;
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
            string sqlDelete = "DELETE FROM song WHERE SongID = @SongID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlDelete, connection);
                    command.Parameters.AddWithValue("@SongID", songID);

                    adapter.DeleteCommand = command;
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

            string sqlInsert = "INSERT INTO Album(Name, Artist, NumberOfTracks) VALUES (@Name, @Artist, @NumberOfTracks)";
            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlInsert, connection);
                    command.Parameters.AddWithValue("@Name", value.Name);
                    command.Parameters.AddWithValue("@Artist", value.Artist);
                    if (value.NumberOfTracks.HasValue)
                        command.Parameters.AddWithValue("@NumberOfTracks", value.NumberOfTracks);
                    else
                        command.Parameters.AddWithValue("@NumberOfTracks", DBNull.Value);

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
            string sqlUpdate = "UPDATE album SET Name = @Name, Artist = @Artist, NumberOfTracks = @NumberOfTracks WHERE AlbumID = @AlbumID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlUpdate, connection);

                    command.Parameters.AddWithValue("@Name", value.Name);
                    command.Parameters.AddWithValue("@Artist", value.Artist);
                    if (value.NumberOfTracks.HasValue)
                        command.Parameters.AddWithValue("@NumberOfTracks", value.NumberOfTracks);
                    else
                        command.Parameters.AddWithValue("@NumberOfTracks", DBNull.Value);

                    command.Parameters.AddWithValue("@AlbumID", albumID);

                    adapter.UpdateCommand = command;
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
            string sqlDelete = String.Format("DELETE FROM album WHERE AlbumID = @AlbumID", albumID);

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlDelete, connection);
                    command.Parameters.AddWithValue("@AlbumID", albumID);

                    adapter.DeleteCommand = command;
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