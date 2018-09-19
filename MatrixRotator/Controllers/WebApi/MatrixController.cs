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
    using Antlr.Runtime.Misc;
    using Services.CSV;
    using Services.Matrix;

    [RoutePrefix("api/matrix")]
    public class MatrixController : ApiController
    {
        private readonly ICsvReaderService csvReaderService;
        private readonly IMatrixRotatorServiceFactory matrixRotatorServiceFactory;

        public MatrixController(ICsvReaderService csvReaderService, IMatrixRotatorServiceFactory matrixRotatorServiceFactory)
        {
            Contract.Requires(csvReaderService != null, nameof(csvReaderService));
            Contract.Requires(matrixRotatorServiceFactory != null, nameof(matrixRotatorServiceFactory));

            this.csvReaderService = csvReaderService;
            this.matrixRotatorServiceFactory = matrixRotatorServiceFactory;
        }

        [HttpPost]
        [Route("rotateRight")]
        public async Task<int[,]> RotateRight()
        {
            return await this.HandleRequest(s => s.RorateRight());
        }

        [HttpPost]
        [Route("rotateLeft")]
        public async Task<int[,]> RotateLeft()
        {
            return await this.HandleRequest(s => s.RorateLeft());
        }

        private async Task<int[,]> HandleRequest(Func<IMatrixRotatorService, int[,]> rotationServiceCall)
        {
            var provider = new MultipartMemoryStreamProvider();
            await this.Request.Content.ReadAsMultipartAsync(provider);
            var files = this.ReadImportRequest(provider);

            if (files.Count <= 0)
            {
                throw new ValidationException("No file choosen");
            }

            var file = files.First();//x => ".csv".Contains(Path.GetExtension(x.Headers.ContentDisposition.FileName.ParseFileName().ToLower()))

            //var fileName = file.Headers.ContentDisposition.FileName;
            var stream = await file.ReadAsStreamAsync();
            var matrix = this.csvReaderService.ReadFile(stream);
            var rotatingService = this.matrixRotatorServiceFactory.GetService(matrix);

            return rotationServiceCall(rotatingService);
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
    }
}