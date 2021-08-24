using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day07.Service.Common;
using Day07.Model.Common;
using Autofac;


namespace Day07.WebApi.Controllers
{
    public class SongController : ApiController
    {
        public SongController(ISongService service)
        {
            //Service = ServiceProvider.Instance.Service<SongService>();
            Service = service;
        }

        private ISongService Service { get; }

        //returns first 100 entries
        [HttpGet]
        [Route("api/Song")]
        public async Task<HttpResponseMessage> GetAsync()
        {
            List<SongREST> songList = (await Service.SelectAllAsync()).ConvertAll(DomainToRestConverter);

            if (songList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, songList);
        }



        //returns entries in range [entryNumber..entryNumber+100]
        [HttpGet]
        [Route("api/Song/id/{id}")]
        public async Task<HttpResponseMessage> GetByIDAsync([FromUri] Guid id)
        {
            SongREST song = DomainToRestConverter(await Service.SelectAsync(id));

            if (song == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, song);

        }

        [HttpGet]
        [Route("api/Song/name/{name}")]
        public async Task<HttpResponseMessage> GetByNameAsync([FromUri] string name)
        {
            //SongREST song = DomainToRestConverter(await Service.SelectAsync(name));
            List<SongREST> songList = (await Service.SelectAllAsync(name)).ConvertAll(DomainToRestConverter);

            //if (song == null)
            if (songList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, songList);

        }


        [HttpPost]
        [Route("api/Song")]
        //both body and uri are legal
        public async Task<HttpResponseMessage> PostAsync([FromUri] SongREST fromUri, [FromBody] SongREST fromBody)
        {
            SongREST song;
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

            if (song.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            int changeCount;
            if (song.SongID.HasValue)
            {
                changeCount = await Service.InsertAsync(Service.NewSong((Guid)song.SongID, song.Name, song.TrackNumber, song.AlbumID));
            }
            else
            {
                changeCount = await Service.InsertAsync(Service.NewSong(song.Name, song.TrackNumber, song.AlbumID));
            }

            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.Created, $"Inserted {changeCount} row(s).");
        }


        [HttpPut]
        [Route("api/song")]
        public async Task<HttpResponseMessage> PutAsync([FromBody] SongREST song)
        {
            if (song.Name == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Body is empty or has invalid data.");
            }

            string action;
            int changeCount;
            if (song.SongID.HasValue)
            {
                ISong temp = Service.NewSong((Guid)song.SongID, song.Name, song.TrackNumber, song.AlbumID);
                action = "Updated";
                changeCount = await Service.UpdateAsync(temp.SongID, temp);
            }
            else
            {
                ISong temp = Service.NewSong(song.Name, song.TrackNumber, song.AlbumID);
                action = "Inserted";
                changeCount = await Service.InsertAsync(temp);
            }

            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, $"{action} {changeCount} row(s).");
        }

        [HttpDelete]
        [Route("api/song/id/{id}")]
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
        [Route("api/song/name/{name}")]
        public async Task<HttpResponseMessage> DeleteByNameAsync([FromUri] string name)
        {
            int changeCount = await Service.DeleteAsync(name);
            if (changeCount == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid data.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Deleted {changeCount} row(s).");
        }

        private SongREST DomainToRestConverter(ISong domain)
        {
            if (domain == null)
                return null;

            return new SongREST(domain.SongID, domain.Name, domain.TrackNumber, domain.AlbumID);
        }
        public class SongREST
        {
            public const int FieldNumber = 4;
            public Guid? SongID { get; set; }
            public string Name { get; set; }
            public int? TrackNumber { get; set; }
            public Guid? AlbumID { get; set; }

            public SongREST() { }
            public SongREST(Guid songGUID, string name, int? trackNum = null, Guid? albumGUID = null)
            {
                SongID = songGUID;
                Name = name;
                TrackNumber = trackNum;
                AlbumID = albumGUID;
            }
        }
    }
}
