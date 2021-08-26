using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day09.Service.Common;
using Day09.Model.Common;
using Day09.Common;
using AutoMapper;


namespace Day09.WebApi.Controllers
{
    public class AlbumController : ApiController
    {
        public AlbumController(IAlbumService service, IMapper mapper)
        {
            //Service = ServiceProvider.Instance.Service<AlbumService>();
            Service = service;
            Mapper = mapper;
        }

        private int Tester { get; set; }
        private IAlbumService Service { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        [Route("api/Album/{pageNum?}")]
        public async Task<HttpResponseMessage> GetAsync([FromUri] FilterCompositeREST metaArg, int pageNum = 1)
        {
            Pager pager;
            Sorter sorter;
            AlbumFilter filter;

            if (metaArg == null)
            {
                pager = new Pager(pageNum);
                sorter = new Sorter();
                filter = new AlbumFilter();
            }
            else
            {
                pager = metaArg.GetPager(pageNum);
                sorter = metaArg.GetSorter();
                filter = metaArg.GetFilter();
            }


            
            //List<AlbumREST> albumList = (await Service.SelectAsync(pager, sorter, filter)).ConvertAll(DomainToRestConverter);
            List<AlbumREST> albumList = Mapper.Map<List<AlbumREST>>(await Service.SelectAsync(pager, sorter, filter));

            if (albumList.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, albumList);
        }

        [HttpGet]
        [Route("api/Album/id/{id}")]
        public async Task<HttpResponseMessage> GetAsync(Guid id)
        {
            AlbumREST album = Mapper.Map<AlbumREST>(await Service.SelectAsync(id));

            if (album == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No entries found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, album);
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
                    return new Pager(Page, PageSize);
                else
                    return new Pager(pageNum, PageSize);
            }

            public Sorter GetSorter()
            {
                return new Sorter(Sort, Order);
            }

            public AlbumFilter GetFilter()
            {
                return new AlbumFilter(Filter);
            }

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
