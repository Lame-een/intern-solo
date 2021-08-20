using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using Day05.Model;
using Day05.Model.Common;
using Day05.Repository.Common;

namespace Day05.Repository
{
    public class SongRepository : ISongRepository
    {
        public async Task<ISong> SelectAsync(Guid id)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM song WHERE SongID = @SongID;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@SongID", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

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


        public async Task<ISong> SelectAsync(string name)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM song WHERE Name LIKE @Name;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@Name", '%' + name + '%');

                SqlDataReader reader = await command.ExecuteReaderAsync();

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

        public async Task<List<ISong>> SelectAllAsync()
        {
            const string sqlSelect = "SELECT * FROM song;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

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

        public async Task<List<ISong>> SelectAllAsync(string name)
        {
            const string sqlSelect = "SELECT * FROM song WHERE Name LIKE @Name;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@Name", '%' + name + '%');

                SqlDataReader reader = await command.ExecuteReaderAsync();

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

        public async Task<int> InsertAsync(ISong song)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            const string sqlInsert = "INSERT INTO Song(SongID, Name, TrackNumber, AlbumID, DateCreated, CreatedBy) VALUES (@ID, @Name, @TrackNumber, @AlbumID, @DateCreated, @CreatedBy)";
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
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@DateCreated", song.DateCreated);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@CreatedBy", song.CreatedBy);

                    adapter.InsertCommand = command;
                    return await adapter.InsertCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task<int> UpdateAsync(Guid id, ISong song)
        {
            const string sqlUpdate = "UPDATE song SET Name = @Name, TrackNumber = @TrackNumber, AlbumID = @AlbumID, DateCreated = @DateCreated, CreatedBy = @CreatedBy WHERE SongID = @SongID";

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
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@DateCreated", song.DateCreated);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@CreatedBy", song.CreatedBy);

                    command.Parameters.AddWithValue("@SongID", id);

                    adapter.UpdateCommand = command;
                    return await adapter.UpdateCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task<int> UpdateAsync(string name, ISong song)
        {
            const string sqlUpdate = "UPDATE song SET Name = @Name, TrackNumber = @TrackNumber, AlbumID = @AlbumID, DateCreated = @DateCreated, CreatedBy = @CreatedBy WHERE Name LIKE @MatchName";

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
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@DateCreated", song.DateCreated);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@CreatedBy", song.CreatedBy);

                    command.Parameters.AddWithValue("@MatchName", '%' + name + '%');

                    adapter.UpdateCommand = command;
                    return await adapter.UpdateCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task<int> DeleteAsync(Guid id)
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
                    return await adapter.DeleteCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task<int> DeleteAsync(string name)
        {
            const string sqlDelete = "DELETE FROM song WHERE Name LIKE @Name";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlDelete, connection);
                    command.Parameters.AddWithValue("@Name", '%' + name + '%');

                    adapter.DeleteCommand = command;
                    return await adapter.DeleteCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

    }
}
