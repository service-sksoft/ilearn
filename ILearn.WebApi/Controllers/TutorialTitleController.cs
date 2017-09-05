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
    using Entities;

    [RoutePrefix("api/tutorialtitle")]
    public class TutorialTitleController : ApiControllerBase
        {
        private readonly ITutorialTitleService tutorialTitleService;

        private HttpResponseMessage response;

        public TutorialTitleController(
            ITutorialTitleService tutorialTitleService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.tutorialTitleService = tutorialTitleService;

            if (GlobalList.TutorialTitle.Count() == 0)
                GlobalList.TutorialTitle = tutorialTitleService.GetAll();
            }

        [Route("questions/{topicid}")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int topicid)
            {
            return CreateHttpResponse(request, () =>
            {
                var allQues = GlobalList.TutorialTitle.Where(x => x.TopicID == topicid).ToList();

                if (allQues != null)
                    {
                    var result = allQues.Select(q => new
                        {
                        id = q.ID,
                        title = q.Title,
                        subtitles = GlobalList.TutorialSubtitle.Where(x => x.ParentID == q.ID).ToList().Select(m => new
                            {
                            id = m.ID,
                            pid = m.ParentID,
                            title = m.Title,
                            seq = m.Seq
                            })
                        }).ToList();

                    response = request.CreateResponse(HttpStatusCode.OK, new { titles = result, total = allQues.Count() });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, topicid.ToString());
                    }

                return response;
            });
            }

        [Route("gettutorialtitle/{TopicID}")]
        [HttpGet]
        public HttpResponseMessage GetAllTopics(HttpRequestMessage request, int TopicID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialTitleService.GetByParentID(TopicID).ToList();

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

        [Route("savetutorialtitle")]
        [HttpPost]
        public HttpResponseMessage SaveTutorialTitle(HttpRequestMessage request, SaveTutorialTitleInputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialTitleService.Save(inputDTO);

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

        [Route("deletetitle/{ID}")]
        [HttpPost]
        public HttpResponseMessage DeleteTutorialTitle(HttpRequestMessage request, int ID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialTitleService.Delete(ID);

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