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
    public class SongController : ApiController
    {
        SongController()
        {
            Service = ServiceProvider.Instance.Service<SongService>();
        }

        private SongService Service { get; }

        //returns first 100 entries
        [HttpGet]
        [Route("api/Song")]
        public HttpResponseMessage Get()
        {
            List<ISong> albumList = Service.SelectAll();

            if (albumList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, albumList);
        }



        //returns entries in range [entryNumber..entryNumber+100]
        [HttpGet]
        [Route("api/Song/id/{id}")]
        public HttpResponseMessage Get([FromUri] Guid id)
        {
            ISong song = Service.Select(id);

            if (song == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, song);

        }

        [HttpGet]
        [Route("api/Song/name/{name}")]
        public HttpResponseMessage Get([FromUri] string name)
        {
            ISong song = Service.Select(name);

            if (song == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, song);

        }


        [HttpPost]
        [Route("api/Song")]
        //both body and uri are legal
        public HttpResponseMessage Post([FromUri] RESTSong fromUri, [FromBody] RESTSong fromBody)
        {
            RESTSong song;
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

            int changeCount = Service.Insert(song);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.Created, $"Inserted {changeCount} row(s).");
        }


        [HttpPut]
        [Route("api/song")]
        public HttpResponseMessage Put([FromBody] RESTSong song)
        {
            int changeCount = Service.Put(song);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"Updates {changeCount} row(s).");
        }

        [HttpDelete]
        [Route("api/song/id/{id}")]
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
        [Route("api/song/name/{name}")]
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
