using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day08.Service.Common;
using Day08.Model.Common;
using Day08.Common;


namespace Day08.WebApi.Controllers
{
    public class SongController : ApiController
    {
        public SongController(ISongService service)
        {
            //Service = ServiceProvider.Instance.Service<SongService>();
            Service = service;
        }

        private ISongService Service { get; }

        [HttpGet]
        [Route("api/Song/{pageNum?}")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] FilterCompositeREST metaArg, int pageNum = 1)
        {
            Pager pager;
            Sorter sorter;
            SongFilter filter;

            if (metaArg == null)
            {
                pager = new Pager(pageNum);
                sorter = new Sorter();
                filter = new SongFilter();
            }
            else
            {
                pager = metaArg.GetPager(pageNum);
                sorter = metaArg.GetSorter();
                filter = metaArg.GetFilter();
            }

            List<SongREST> songList = (await Service.SelectAsync(pager, sorter, filter)).ConvertAll(DomainToRestConverter);

            if (songList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, songList);
        }

        [HttpGet]
        [Route("api/Song/id/{id}")]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            SongREST song = DomainToRestConverter(await Service.SelectAsync(id));

            if (song == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, song);
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

        //JObject might be a better solution - research into it for full project
        public class FilterCompositeREST
        {
            //filter unwrap
            public string Filter { get; set; }

            //sorter unwrap
            public string Sort { get; set; }
            public Sorter.OrderType Order { get; set; }

            //pager unwrap
            public int Page { get; set; }
            public int PageSize { get; set; }

            public Pager GetPager(int pageNum = 0)
            {
                if (Page != 0)
                    return new Pager(PageSize, Page);
                else
                    return new Pager(PageSize, pageNum);
            }

            public Sorter GetSorter()
            {
                return new Sorter(Sort, Order);
            }

            public SongFilter GetFilter()
            {
                return new SongFilter(Filter);
            }

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
