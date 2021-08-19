using System;
using System.Collections.Generic;
using Day04.Model;
using Day04.Model.Common;
using Day04.Repository.Common;
using System.Data.SqlClient;

namespace Day04.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        public IAlbum Select(Guid id)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM album WHERE AlbumID = @AlbumID;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@AlbumID", id);

                SqlDataReader reader = command.ExecuteReader();

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


        public IAlbum Select(string name)
        {
            const string sqlSelect = "SELECT TOP(1) * FROM album WHERE Name = @Name;";

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

        public List<IAlbum> SelectAll()
        {
            const string sqlSelect = "SELECT * FROM album;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                SqlDataReader reader = command.ExecuteReader();

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

        public List<IAlbum> SelectAll(Guid id)
        {
            const string sqlSelect = "SELECT * FROM album WHERE AlbumID = @AlbumID;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@AlbumID", id);

                SqlDataReader reader = command.ExecuteReader();

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

        public List<IAlbum> SelectAll(string name)
        {
            const string sqlSelect = "SELECT * FROM album WHERE Name = @Name;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlSelect, connection);
                command.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = command.ExecuteReader();

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

        public int Insert(IAlbum album)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();

            const string sqlInsert = "INSERT INTO Album(AlbumID, Name, Artist, NumberOfTracks) VALUES (@AlbumID, @Name, @Artist, @NumberOfTracks)";
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

                    //if (album.NumberOfTracks.HasValue)
                    //    command.Parameters.AddWithValue("@NumberOfTracks", album.NumberOfTracks);
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

        public int Update(Guid id, IAlbum album)
        {
            const string sqlUpdate = "UPDATE album SET Name = @Name, Artist = @Artist, NumberOfTracks = @NumberOfTracks WHERE AlbumID = @AlbumID";

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
                    //if (album.NumberOfTracks.HasValue)
                    //    command.Parameters.AddWithValue("@NumberOfTracks", album.NumberOfTracks);
                    //else
                    //    command.Parameters.AddWithValue("@NumberOfTracks", DBNull.Value);

                    command.Parameters.AddWithValue("@AlbumID", id);

                    adapter.UpdateCommand = command;
                    return adapter.UpdateCommand.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public int Update(string name, IAlbum album)
        {
            const string sqlUpdate = "UPDATE album SET Name = @Name, Artist = @Artist, NumberOfTracks = @NumberOfTracks WHERE Name = @MatchName";

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
                    //if (album.NumberOfTracks.HasValue)
                    //    command.Parameters.AddWithValue("@NumberOfTracks", album.NumberOfTracks);
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
            const string sqlDelete = "DELETE FROM album WHERE Name = @Name";

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
