namespace ILearn.WebApi.Controllers
    {
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ILearn.Data.Infrastructure;
    using ILearn.WebApi.Infrastructure.Core;
    using Services.TutorialTitle;
    using Services.TutorialTitle.Dtos;
    using Services.TutorialSubtitle;
    using Services.TutorialSubtitle.Dtos;
    using Entities;

    [RoutePrefix("api/tutorialsubtitle")]
    public class TutorialSubtitleController : ApiControllerBase
        {
        private readonly ITutorialSubtitleService tutorialsubtitleService;

        private HttpResponseMessage response;

        public TutorialSubtitleController(ITutorialSubtitleService tutorialsubtitleService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.tutorialsubtitleService = tutorialsubtitleService;

            if (GlobalList.TutorialSubtitle.Count() == 0)
                GlobalList.TutorialSubtitle = tutorialsubtitleService.GetAll();

            }

        [Route("gettutorialsubtitle/{ParentID}")]
        [HttpGet]
        public HttpResponseMessage GetAllTopics(HttpRequestMessage request, int ParentID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialsubtitleService.GetByParentID(ParentID).ToList();

                if (result != null)
                    {
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }

                return response;
            });
            }

        [Route("savetutorialsubtitle")]
        [HttpPost]
        public HttpResponseMessage SaveTutorialSubTitle(HttpRequestMessage request, SaveTutorialSubtitleInputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialsubtitleService.Save(inputDTO);

                if (result)
                    {
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }

                return response;
            });
            }

        [Route("deletesubtitle/{ID}")]
        [HttpPost]
        public HttpResponseMessage DeleteTutorialTitle(HttpRequestMessage request, int ID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialsubtitleService.Delete(ID);

                if (result)
                    {
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }
                return response;
            });
            }


        }
    }