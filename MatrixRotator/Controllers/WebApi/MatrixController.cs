namespace MatrixRotator.Controllers.WebApi
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Services.CSV;

    [RoutePrefix("api/matrix")]
    public class MatrixController : ApiController
    {
        private readonly ICsvReaderService csvReaderService;

        public MatrixController(ICsvReaderService csvReaderService)
        {
            Contract.Requires(csvReaderService != null, nameof(csvReaderService));

            this.csvReaderService = csvReaderService;
        }

        [HttpGet, Route("get")]
        public string get()
        {
            return "ok";
        }

        [HttpPost]
        [Route("rotate")]
        public async Task Rotate()
        {
            var provider = new MultipartMemoryStreamProvider();
            await this.Request.Content.ReadAsMultipartAsync(provider);
            var files = this.ReadImportRequest(provider);

            if (files.Count <= 0)
            {
                throw new ValidationException("No file choosen");
            }

            var file = files.FirstOrDefault();//x => ".csv".Contains(Path.GetExtension(x.Headers.ContentDisposition.FileName.ParseFileName().ToLower()))

            if (file != null)
            {
                var fileName = file.Headers.ContentDisposition.FileName;
                var stream = await file.ReadAsStreamAsync();
                this.csvReaderService.ReadFile(stream);
            }
        }


        private ICollection<HttpContent> ReadImportRequest(MultipartMemoryStreamProvider provider)
        {
            #region media

            var media = new List<string>
            {
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "application/octet-stream",
                "application/vnd.ms-excel",
                "text/xml",
                "application/json",
                ".csv"
            };

            #endregion
            // media.Any(x => x.Equals(c.Headers.ContentType.MediaType.ToLower())
            return
                provider.Contents;
                //.Where(
                //        c => c.Headers.ContentType.MediaType == "application/vnd.ms-excel")
                //    .ToList();
        }
        private async Task<bool> CheckIfRight(MultipartMemoryStreamProvider provider)
        {
            var requestContent = provider.Contents.First(c => c.Headers.ContentType.MediaType == "application/json" && c.Headers.ContentDisposition?.Name == "\"isRight\"");
            var json = await requestContent.ReadAsStringAsync();
            return json == "false";
        }
    }
}