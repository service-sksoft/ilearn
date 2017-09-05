namespace ILearn.WebApi.Controllers
    {
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ILearn.Data.Infrastructure;
    using ILearn.WebApi.Infrastructure.Core;
    using Services.Topic;
    using System.Web;
    using System;
    using System.IO;

    [RoutePrefix("api/fileupload")]
    public class UploadContentController : ApiControllerBase
        {
        private string UploadFolder = @"Uploads";
        private string TopicFolder = @"topics";

        private HttpResponseMessage response;

        public UploadContentController(IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            }


        [Route("uploadfile/{ID}")]
        [HttpPost]
        public HttpResponseMessage UploadFile(HttpRequestMessage request, int ID, [FromUri] string Type)
            {
            HttpPostedFile file = HttpContext.Current.Request.Files.Count > 0
               ? HttpContext.Current.Request.Files[0]
               : null;

            return CreateHttpResponse(request, () =>
            {
                var result = FileUpload(file, Type, ID);
                response = request.CreateResponse(HttpStatusCode.OK, new { result });
                return response;
            });
            }

        private bool FileUpload(HttpPostedFile file, string Type, int ID)
            {
            try
                {
                string filePath = null;
                string folderPath = null;

                if (file.ContentLength > 0)
                    {
                    switch (Type)
                        {
                        case "topic":
                        folderPath = Path.Combine(this.UploadFolder, this.TopicFolder);
                        filePath = folderPath + "/topic_" + ID + ".png";
                        break;
                        }
                    this.CreateFolder(folderPath);
                    file.SaveAs(HttpContext.Current.Server.MapPath(@"~\" + filePath));
                    }

                return true;
                }
            catch (Exception ex)
                {
                return false;
                }
            }

        private void CreateFolder(string folderPath)
            {
            folderPath = HttpContext.Current.Server.MapPath(@"~\" + folderPath);
            if (!Directory.Exists(folderPath))
                {
                Directory.CreateDirectory(folderPath);
                }
            }
        }
    }