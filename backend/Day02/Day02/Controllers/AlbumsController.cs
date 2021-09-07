using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day02.Models;

namespace Day02.Controllers
{
    public class AlbumsController : ApiController
    {
        //[Route("api/Albums")] - default route has optional albumName
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Globals.AlbumDictionary);
        }

        //[Route("api/Albums/albumName")] - default route
        [HttpGet]
        public HttpResponseMessage Get(string albumName)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, Globals.AlbumDictionary[albumName.ToLower()]);
            }
            catch (KeyNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Cannot find {albumName}");
            }
        }

        [HttpPost]
        //[Route("api/Albums")]
        //both body and uri are legal
        public HttpResponseMessage Post([FromUri] Album fromUri, [FromBody] Album fromBody)
        {
            Album album;
            if (fromBody != null)
            {
                album = fromBody;
            }
            else if ((fromUri.Name != null) && (fromUri.Artist != null))
            {
                album = fromUri;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            if (Globals.AlbumDictionary.ContainsKey(album.Name.ToLower()))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, $"{album.Name} already exists.");
            }

            Globals.AlbumDictionary[album.Name.ToLower()] = album;

            return Request.CreateResponse(HttpStatusCode.Created, album);
        }


        //playing around with routes
        [HttpPost]
        [Route("api/Albums/{name}/{artist}/{numberOfTracks}")]
        public HttpResponseMessage Post([FromUri] string name, [FromUri] string artist, [FromUri] int numberOfTracks)
        {
            if (Globals.AlbumDictionary.ContainsKey(name.ToLower()))
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, $"{name} already exists.");
            }

            Globals.AlbumDictionary[name.ToLower()] = new Album(name, artist, numberOfTracks);

            return Request.CreateResponse(HttpStatusCode.OK, Globals.AlbumDictionary[name.ToLower()]);
        }

        [HttpPut]
        public HttpResponseMessage Put([FromUri] string albumName, [FromBody] Album value)
        {
            if ((value.Name == null) || (value.Artist == null))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            Globals.AlbumDictionary[albumName.ToLower()] = value;
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] string albumName)
        {
            if (Globals.AlbumDictionary.ContainsKey(albumName.ToLower()))
            {
                Globals.AlbumDictionary.Remove(albumName.ToLower());
                return Request.CreateResponse(HttpStatusCode.OK, $"{albumName} has been deleted.");
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, $"Cannot find {albumName}.");
        }
    }
}
