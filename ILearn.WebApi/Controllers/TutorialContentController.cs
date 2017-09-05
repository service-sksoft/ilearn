namespace ILearn.WebApi.Controllers
    {
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ILearn.Data.Infrastructure;
    using ILearn.WebApi.Infrastructure.Core;
    using Services.TutorialContent;
    using Services.TutorialContent.Dtos;
    using Entities;
    using System.Collections.Generic;
    using Services.Common;

    [RoutePrefix("api/tutorialcontent")]
    public class TutorialContentController : ApiControllerBase
        {
        private readonly ITutorialContentService tutorialContentService;

        private HttpResponseMessage response;

        public TutorialContentController(
            ITutorialContentService tutorialContentService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.tutorialContentService = tutorialContentService;

            if (GlobalList.TutorialContent.Count() == 0)
                GlobalList.TutorialContent = tutorialContentService.GetAll();

            }

        [Route("tutorialcontent/{topicid}")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int topicid)
            {
            return CreateHttpResponse(request, () =>
            {
                var allQues = GlobalList.TutorialContent.Where(x => x.ParentID == topicid).OrderBy(m => m.Seq).ToList();

                if (allQues != null)
                    {
                    var result = allQues.Select(q => new
                        {
                        id = q.ID,
                        seq = q.Seq,
                        pId = q.ParentID,
                        title = q.Title,
                        desc = q.DescriptionString,
                        type = q.Type
                        }).ToList();

                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, topicid.ToString());
                    }

                return response;
            });
            }


        [Route("gettutorialcontent/{ParentID}")]
        [HttpGet]
        public HttpResponseMessage GetTutorialContent(HttpRequestMessage request, int ParentID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.GetTutorialContentOutputDto(this.tutorialContentService.GetByParentID(ParentID).ToList());

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

        [Route("savetutorialcontent")]
        [HttpPost]
        public HttpResponseMessage SaveTutorialContent(HttpRequestMessage request, SaveTutorialContentInputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialContentService.Save(inputDTO);

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

        [Route("deletecontent/{ID}")]
        [HttpPost]
        public HttpResponseMessage DeleteTutorialContent(HttpRequestMessage request, int ID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.tutorialContentService.Delete(ID);

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

        private List<TutorialContentOutputDto> GetTutorialContentOutputDto(List<TutorialContent> topicList)
            {
            return topicList.Select(q => new TutorialContentOutputDto()
                {
                ID = q.ID,
                ParentID = q.ParentID,
                Seq = q.Seq,
                Title = q.Title,
                Description = Cmn.GetUnCompressed(q.Description, q.DescriptionLength),
                Type = q.Type
                }).ToList();
            }

        }
    }