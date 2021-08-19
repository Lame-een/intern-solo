using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Day04.Model;
using Day04.Model.Common;
using Day04.Repository.Common;

namespace Day04.Repository
{
    public class SongRepository : ISongRepository
    {
        public ISong Select(Guid id)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM song WHERE SongID = @SongID;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@SongID", id);

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                Song ret = null;

                object[] objectBuffer = new object[Song.FieldNumber];
                if (reader.Read())
                {
                    reader.GetValues(objectBuffer);
                    ret = new Song(objectBuffer);
                }
                return ret;
            }
        }


        public ISong Select(string name)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM song WHERE Name = @Name;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                Song ret = null;

                object[] objectBuffer = new object[Song.FieldNumber];
                if (reader.Read())
                {
                    reader.GetValues(objectBuffer);
                    ret = new Song(objectBuffer);
                }
                return ret;
            }
        }

        public List<ISong> SelectAll()
        {
            const string sqlSelect = "SELECT * FROM song;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<ISong> songList = new List<ISong>();

                if (!reader.HasRows)
                {
                    return songList;
                }

                object[] objectBuffer = new object[Song.FieldNumber];
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValues(objectBuffer);
                        songList.Add(new Song(objectBuffer));
                    }
                    reader.NextResult();
                }
                return songList;
            }
        }

        public List<ISong> SelectAll(Guid id)
        {
            const string sqlSelect = "SELECT * FROM song WHERE SongID = @SongID;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@SongID", id);

                SqlDataReader reader = command.ExecuteReader();

                List<ISong> songList = new List<ISong>();

                if (!reader.HasRows)
                {
                    return songList;
                }

                object[] objectBuffer = new object[Song.FieldNumber];
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValues(objectBuffer);
                        songList.Add(new Song(objectBuffer));
                    }
                    reader.NextResult();
                }
                return songList;
            }
        }

        public List<ISong> SelectAll(string name)
        {
            const string sqlSelect = "SELECT * FROM song WHERE Name = @Name;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = command.ExecuteReader();

                List<ISong> songList = new List<ISong>();

                if (!reader.HasRows)
                {
                    return songList;
                }

                object[] objectBuffer = new object[Song.FieldNumber];
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValues(objectBuffer);
                        songList.Add(new Song(objectBuffer));
                    }
                    reader.NextResult();
                }
                return songList;
            }
        }

        public int Insert(ISong song)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            const string sqlInsert = "INSERT INTO Song(SongID, Name, TrackNumber, AlbumID) VALUES (@ID, @Name, @TrackNumber, @AlbumID)";
            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlInsert, connection);
                    command.Prepare();

                    command.Parameters.AddWithValue("@ID", song.SongID);
                    command.Parameters.AddWithValue("@Name", song.Name);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@TrackNumber", song.TrackNumber);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@AlbumID", song.AlbumID);

                    //if (song.NumberOfTracks.HasValue)
                    //    command.Parameters.AddWithValue("@NumberOfTracks", song.NumberOfTracks);
                    //else
                    //    command.Parameters.AddWithValue("@NumberOfTracks", DBNull.Value);

                    adapter.InsertCommand = command;
                    return adapter.InsertCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public int Update(Guid id, ISong song)
        {
            const string sqlUpdate = "UPDATE song SET Name = @Name, TrackNumber = @TrackNumber, AlbumID = @AlbumID WHERE SongID = @SongID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlUpdate, connection);

                    command.Parameters.AddWithValue("@Name", song.Name);

                    SqlUtilities.AddParameterWithNullableValue(ref command, "@TrackNumber", song.TrackNumber);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@AlbumID", song.AlbumID);
                    //if (song.NumberOfTracks.HasValue)
                    //    command.Parameters.AddWithValue("@NumberOfTracks", song.NumberOfTracks);
                    //else
                    //    command.Parameters.AddWithValue("@NumberOfTracks", DBNull.Value);

                    command.Parameters.AddWithValue("@SongID", id);

                    adapter.UpdateCommand = command;
                    return adapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public int Update(string name, ISong song)
        {
            const string sqlUpdate = "UPDATE song SET Name = @Name, TrackNumber = @TrackNumber, AlbumID = @AlbumID WHERE Name = @MatchName";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlUpdate, connection);

                    command.Parameters.AddWithValue("@Name", song.Name);

                    SqlUtilities.AddParameterWithNullableValue(ref command, "@TrackNumber", song.TrackNumber);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@AlbumID", song.AlbumID);
                    //if (song.NumberOfTracks.HasValue)
                    //    command.Parameters.AddWithValue("@NumberOfTracks", song.NumberOfTracks);
                    //else
                    //    command.Parameters.AddWithValue("@NumberOfTracks", DBNull.Value);

                    command.Parameters.AddWithValue("@MatchName", name);

                    adapter.UpdateCommand = command;
                    return adapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public int Delete(Guid id)
        {
            const string sqlDelete = "DELETE FROM song WHERE SongID = @SongID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlDelete, connection);
                    command.Parameters.AddWithValue("@SongID", id);

                    adapter.DeleteCommand = command;
                    return adapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public int Delete(string name)
        {
            const string sqlDelete = "DELETE FROM song WHERE Name = @Name";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlDelete, connection);
                    command.Parameters.AddWithValue("@Name", name);

                    adapter.DeleteCommand = command;
                    return adapter.DeleteCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

    }
}
