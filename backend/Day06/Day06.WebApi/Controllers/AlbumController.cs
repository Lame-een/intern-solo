using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day06.Service.Common;
using Day06.Model.Common;
using Autofac;


namespace Day06.WebApi.Controllers
{
    public class AlbumController : ApiController
    {
        public AlbumController(IAlbumService service)
        {
            //Service = ServiceProvider.Instance.Service<AlbumService>();
            Service = service;
        }

        private IAlbumService Service { get; }

        [HttpGet]
        [Route("api/Album")]
        public async Task<HttpResponseMessage> GetAsync()
        {
            List<AlbumREST> albumList = (await Service.SelectAllAsync()).ConvertAll(DomainToRestConverter);

            if (albumList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }


            return Request.CreateResponse(HttpStatusCode.OK, albumList);
        }


        [HttpGet]
        [Route("api/Album/id/{id}")]
        public async Task<HttpResponseMessage> GetByIDAsync(Guid albumID)
        {
            AlbumREST album = DomainToRestConverter(await Service.SelectAsync(albumID));

            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, album);

        }

        [HttpGet]
        [Route("api/Album/name/{name}")]
        public async Task<HttpResponseMessage> GetByNameAsync(string name)
        {
            List<AlbumREST> albumList = (await Service.SelectAllAsync(name)).ConvertAll(DomainToRestConverter);

            if (albumList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, albumList);
        }


        [HttpPost]
        [Route("api/Album")]
        public async Task<HttpResponseMessage> PostAsync([FromUri] AlbumREST fromUri, [FromBody] AlbumREST fromBody)
        {
            AlbumREST album;
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

            if (album.Name.Length == 0 || album.Artist.Length == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            int changeCount;
            if (album.AlbumID.HasValue)
            {
                changeCount = await Service.InsertAsync(Service.NewAlbum((Guid)album.AlbumID, album.Name, album.Artist, album.NumberOfTracks));
            }
            else
            {
                changeCount = await Service.InsertAsync(Service.NewAlbum(album.Name, album.Artist, album.NumberOfTracks));
            }

            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.Created, $"Inserted {changeCount} row(s).");
        }


        [HttpPut]
        [Route("api/album")]
        public async Task<HttpResponseMessage> PutAsync([FromBody] AlbumREST album)
        {
            if (album.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            string action;
            int changeCount;
            if (album.AlbumID.HasValue)
            {
                IAlbum temp = Service.NewAlbum((Guid)album.AlbumID, album.Name, album.Artist, album.NumberOfTracks);
                action = "Updated";
                changeCount = await Service.UpdateAsync(temp.AlbumID, temp);
            }
            else
            {
                action = "Inserted";
                changeCount = await Service.InsertAsync(Service.NewAlbum(album.Name, album.Artist, album.NumberOfTracks));
            }

            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"{action} {changeCount} row(s).");

        }

        [HttpDelete]
        [Route("api/album/id/{id}")]
        public async Task<HttpResponseMessage> DeleteByIDAsync([FromUri] Guid id)
        {
            int changeCount = await Service.DeleteAsync(id);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }

        [HttpDelete]
        [Route("api/album/name/{name}")]
        public async Task<HttpResponseMessage> DeleteByNameAsync([FromUri] string name)
        {
            int changeCount = await Service.DeleteAsync(name);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }

        private AlbumREST DomainToRestConverter(IAlbum domain)
        {
            if (domain == null)
                return null;
            return new AlbumREST(domain.AlbumID, domain.Name, domain.Artist, domain.NumberOfTracks);
        }
        public class AlbumREST
        {
            public const int FieldNumber = 4;
            public Guid? AlbumID { get; set; }
            public string Name { get; set; }
            public string Artist { get; set; }
            public int? NumberOfTracks { get; set; }

            public AlbumREST() { }
            public AlbumREST(Guid albumGUID, string name, string artist, int? trackNum)
            {
                AlbumID = albumGUID;
                Name = name;
                Artist = artist;
                NumberOfTracks = trackNum;
            }
        }
    }
}
