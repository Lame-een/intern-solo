using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Day09.Model;
using Day09.Model.Common;
using Day09.Repository.Common;
using System.Data.SqlClient;
using Day09.Common;

namespace Day09.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public async Task<IAlbum> SelectAsync(Guid id)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM album WHERE AlbumID = @AlbumID;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@AlbumID", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return null;
                }

                Album ret = null;

                object[] objectBuffer = new object[Album.FieldNumber];
                if (reader.Read())
                {
                    reader.GetValues(objectBuffer);
                    ret = new Album(objectBuffer);
                }
                return ret;
            }
        }


        public async Task<List<IAlbum>> SelectAsync(Pager pager, Sorter sorter, AlbumFilter filter)
        {
            string sqlSelect = "SELECT * FROM album" + filter.GetSql() + sorter.GetSql(typeof(IAlbum)) + pager.GetSql() + ';';

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);

                pager.AddParameters(ref command);
                filter.AddParameters(ref command);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                List<IAlbum> albumList = new List<IAlbum>();

                if (!reader.HasRows)
                {
                    return albumList;
                }

                object[] objectBuffer = new object[Album.FieldNumber];
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValues(objectBuffer);
                        albumList.Add(new Album(objectBuffer));
                    }
                    reader.NextResult();
                }
                return albumList;
            }
        }


        public async Task<int> InsertAsync(IAlbum album)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            const string sqlInsert = "INSERT INTO Album(AlbumID, Name, Artist, NumberOfTracks, DateCreated, CreatedBy) VALUES (@AlbumID, @Name, @Artist, @NumberOfTracks, @DateCreated, @CreatedBy)";
            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlInsert, connection);
                    command.Parameters.AddWithValue("@AlbumID", album.AlbumID);
                    command.Parameters.AddWithValue("@Name", album.Name);
                    command.Parameters.AddWithValue("@Artist", album.Artist);

                    SqlUtilities.AddParameterWithNullableValue(ref command, "@NumberOfTracks", album.NumberOfTracks);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@DateCreated", album.DateCreated);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@CreatedBy", album.CreatedBy);

                    adapter.InsertCommand = command;
                    return await adapter.InsertCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task<int> UpdateAsync(Guid id, IAlbum album)
        {
            const string sqlUpdate = "UPDATE album SET Name = @Name, Artist = @Artist, NumberOfTracks = @NumberOfTracks, DateCreated = @DateCreated, CreatedBy = @CreatedBy WHERE AlbumID = @AlbumID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlUpdate, connection);

                    command.Parameters.AddWithValue("@Name", album.Name);
                    command.Parameters.AddWithValue("@Artist", album.Artist);

                    SqlUtilities.AddParameterWithNullableValue(ref command, "@NumberOfTracks", album.NumberOfTracks);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@DateCreated", album.DateCreated);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@CreatedBy", album.CreatedBy);

                    command.Parameters.AddWithValue("@AlbumID", id);

                    adapter.UpdateCommand = command;
                    return await adapter.UpdateCommand.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task<int> UpdateAsync(string name, IAlbum album)
        {
            const string sqlUpdate = "UPDATE album SET Name = @Name, Artist = @Artist, NumberOfTracks = @NumberOfTracks, DateCreated = @DateCreated, CreatedBy = @CreatedBy WHERE Name LIKE @MatchName";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlUpdate, connection);

                    command.Parameters.AddWithValue("@Name", album.Name);
                    command.Parameters.AddWithValue("@Artist", album.Artist);

                    SqlUtilities.AddParameterWithNullableValue(ref command, "@NumberOfTracks", album.NumberOfTracks);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@DateCreated", album.DateCreated);
                    SqlUtilities.AddParameterWithNullableValue(ref command, "@CreatedBy", album.CreatedBy);

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
            const string sqlDelete = "DELETE FROM album WHERE AlbumID = @AlbumID";

            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlDelete, connection);
                    command.Parameters.AddWithValue("@AlbumID", id);

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
            const string sqlDelete = "DELETE FROM album WHERE Name LIKE @Name";

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
