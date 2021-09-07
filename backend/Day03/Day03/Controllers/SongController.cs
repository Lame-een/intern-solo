using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day03.App_Code;
using System.Data.SqlClient;
using Day03.Models;

namespace Day03.Controllers
{
    public class SongController : ApiController
    {
        //[Route("api/Song")] - default route has optional albumName
        //returns first 100 entries
        [HttpGet]
        public HttpResponseMessage Get()
        {
            const string commandString = "SELECT SongID, Name, TrackNumber, AlbumID FROM (SELECT ROW_NUMBER() OVER(ORDER BY AlbumID ASC) AS rownumber, * FROM dbo.song) AS foo WHERE rownumber <= 100;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandString, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
                }

                List<Song> songList = new List<Song>();

                object[] objectBuffer = new object[4];
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValues(objectBuffer);
                        songList.Add(new Song(objectBuffer));
                    }
                    reader.NextResult();
                }
                return Request.CreateResponse(HttpStatusCode.OK, songList);
            }
        }


        //returns entries in range [entryNumber..entryNumber+100]
        [HttpGet]
        [Route("api/Song/{songGUID}")]
        public HttpResponseMessage Get(Guid songGUID)
        {
            string commandString = String.Format("SELECT * FROM song WHERE SongID = '{0}'", songGUID);

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandString, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
                }


                object[] objectBuffer = new object[4];
                while (reader.Read())
                {
                    reader.GetValues(objectBuffer);
                }
                Song song = new Song(objectBuffer);

                return Request.CreateResponse(HttpStatusCode.OK, song);
            }
        }


        [HttpPost]
        //[Route("api/Song")]
        //both body and uri are legal
        public HttpResponseMessage Post([FromUri] Song fromUri, [FromBody] Song fromBody)
        {
            Song song;
            if (fromBody != null)
            {
                song = fromBody;
            }
            else if (fromUri.Name != null)
            {
                song = fromUri;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            int changeCount = DatabaseHandler.InsertSong(song);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.Created, $"Inserted {changeCount} row(s).");
        }

        [HttpPut]
        [Route("api/song/{songGUID}")]
        public HttpResponseMessage Put([FromUri] Guid songGUID, [FromBody] Song value)
        {
            if (value.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            if (songGUID == null)
            {
                int changeCount = DatabaseHandler.InsertSong(value);
                if (changeCount == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Inserted {changeCount} row(s)");
            }
            else
            {
                int changeCount = DatabaseHandler.UpdateSong(songGUID, value);
                if (changeCount == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Updates {changeCount} row(s).");
            }
        }

        [HttpDelete]
        [Route("api/song/{songGUID}")]
        public HttpResponseMessage Delete([FromUri] Guid songGUID)
        {
            int changeCount = DatabaseHandler.DeleteSong(songGUID);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }
    }
}
