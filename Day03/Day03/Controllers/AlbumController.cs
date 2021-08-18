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
    public class AlbumController : ApiController
    {
        //[Route("api/Album")] - default route has optional albumName
        //returns first 100 entries
        [HttpGet]
        public HttpResponseMessage Get()
        {
            const string commandString = "SELECT AlbumID, Name, Artist, NumberOfTracks FROM (SELECT ROW_NUMBER() OVER(ORDER BY AlbumID ASC) AS rownumber, * FROM dbo.album) AS foo WHERE rownumber <= 100;";

            using (SqlConnection connection = DatabaseHandler.Instance.NewConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandString, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
                }

                List<Album> albumList = new List<Album>();

                object[] objectBuffer = new object[4];
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValues(objectBuffer);
                        albumList.Add(new Album(objectBuffer));
                    }
                    reader.NextResult();
                }
                return Request.CreateResponse(HttpStatusCode.OK, albumList);
            }
        }


        //returns entries in range [entryNumber..entryNumber+100]
        [HttpGet]
        [Route("api/Album/{albumGUID}")]
        public HttpResponseMessage Get(Guid albumGUID)
        {
            string commandString = String.Format("SELECT * FROM album WHERE AlbumID = '{0}'", albumGUID);

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
                Album album = new Album(objectBuffer);

                return Request.CreateResponse(HttpStatusCode.OK, album);
            }
        }


        [HttpPost]
        //[Route("api/Album")]
        //both body and uri are legal
        public HttpResponseMessage Post([FromUri] Album fromUri, [FromBody] Album fromBody)
        {
            Album album;
            if (fromBody != null)
            {
                album = fromBody;
            }
            else if (fromUri.Name != null)
            {
                album = fromUri;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            int changeCount = DatabaseHandler.InsertAlbum(album);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.Created, $"Inserted {changeCount} row(s).");
        }

        [HttpPut]
        [Route("api/album/{albumGUID}")]
        public HttpResponseMessage Put([FromUri] Guid albumGUID, [FromBody] Album value)
        {
            if (value.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            if (albumGUID == null)
            {
                int changeCount = DatabaseHandler.InsertAlbum(value);
                if (changeCount == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Inserted {changeCount} row(s)");
            }
            else
            {
                int changeCount = DatabaseHandler.UpdateAlbum(albumGUID, value);
                if (changeCount == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Updates {changeCount} row(s).");
            }
        }

        [HttpDelete]
        [Route("api/album/{albumGUID}")]
        public HttpResponseMessage Delete([FromUri] Guid albumGUID)
        {
            int changeCount = DatabaseHandler.DeleteAlbum(albumGUID);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }
    }
}
