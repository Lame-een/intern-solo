using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day04.Service;
using Day04.Model.Common;
using Day04.Model;


namespace Day04.WebApi.Controllers
{
    public class AlbumController : ApiController
    {
        AlbumController()
        {
            Service = ServiceProvider.Instance.Service<AlbumService>();
        }

        private AlbumService Service { get; }

        [HttpGet]
        [Route("api/Album")]
        public HttpResponseMessage Get()
        {
            List<IAlbum> albumList = Service.SelectAll();

            if (albumList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }


            return Request.CreateResponse(HttpStatusCode.OK, albumList);
        }


        [HttpGet]
        [Route("api/Album/id")]
        public HttpResponseMessage Get(Guid albumID)
        {
            IAlbum album = Service.Select(albumID);

            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, album);

        }

        [HttpGet]
        [Route("api/Album/name")]
        public HttpResponseMessage Get(string name)
        {
            IAlbum album = Service.Select(name);

            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, album);
        }


        [HttpPost]
        public HttpResponseMessage Post([FromUri] RESTAlbum fromUri, [FromBody] RESTAlbum fromBody)
        {
            RESTAlbum album;
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

            int changeCount = Service.Insert(album);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.Created, $"Inserted {changeCount} row(s).");
        }


        [HttpPut]
        [Route("api/album")]
        public HttpResponseMessage Put([FromBody] RESTAlbum album)
        {
            
                int changeCount = Service.Put(album);
                if (changeCount == -1)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, $"Updates {changeCount} row(s).");
            
        }

        [HttpDelete]
        [Route("api/album/id/{id}")]
        public HttpResponseMessage Delete([FromUri] Guid id)
        {
            int changeCount = Service.Delete(id);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }

        [HttpDelete]
        [Route("api/album/name/{name}")]
        public HttpResponseMessage Delete([FromUri] string name)
        {
            int changeCount = Service.Delete(name);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }
    }
}
