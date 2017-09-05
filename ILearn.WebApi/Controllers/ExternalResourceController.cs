namespace ILearn.WebApi.Controllers
    {
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.WebApi.Infrastructure.Core;
    using Services.Topic;
    using Services.Topic.Dtos;
    using System.Collections.Generic;
    using Entities;
    using Services.Common;
    using Services.ExternalResource;
    using Services.ExternalResource.Dtos;

    [RoutePrefix("api/externalresource")]
    public class ExternalResourceController : ApiControllerBase
        {
        private readonly IExternalResourceService externalResourceService;
        private HttpResponseMessage response;

        public ExternalResourceController(
            IExternalResourceService externalResourceService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.externalResourceService = externalResourceService;

            if (GlobalList.ExternalResources.Count() == 0)
                GlobalList.ExternalResources = externalResourceService.GetAll();
            }


        [Route("questions/{topicid}/{pageNo}")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int topicid, int pageNo)
            {
            return CreateHttpResponse(request, () =>
            {
                var allQues = GlobalList.ExternalResources.Where(x => x.TopicID == topicid).ToList();
                var quesToReturn = allQues.Skip((pageNo - 1) * Cmn.MaxQuestionPerPage).Take(Cmn.MaxQuestionPerPage).ToList();

                if (quesToReturn != null)
                    {
                    var result = this.GetQuesListOutputDto(quesToReturn);
                    response = request.CreateResponse(HttpStatusCode.OK, new { ques = result, total = allQues.Count(), currentpage = pageNo, perpage = Cmn.MaxQuestionPerPage });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, topicid.ToString());
                    }

                return response;
            });
            }


        [Route("{TopicID}")]
        [HttpGet]
        public HttpResponseMessage GetReferenceByTopicID(HttpRequestMessage request, int TopicID)
            {
            return CreateHttpResponse(request, () =>
            {
                var results = this.externalResourceService.GetByTopicID(TopicID);

                if (results != null)
                    {
                    response = request.CreateResponse(HttpStatusCode.OK, new { results });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }

                return response;
            });
            }

        [HttpPost]
        public HttpResponseMessage SaveReference(HttpRequestMessage request, SaveExternalResourceInputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var results = this.externalResourceService.SaveReference(inputDTO);
                response = request.CreateResponse(HttpStatusCode.OK, new { results });
                return response;
            });
            }

        [Route("delete/{ID}")]
        [HttpPost]
        public HttpResponseMessage DeletesReference(HttpRequestMessage request, int ID)
            {
            return CreateHttpResponse(request, () =>
            {
                var results = this.externalResourceService.DeleteReference(ID);
                response = request.CreateResponse(HttpStatusCode.OK, new { results });
                return response;
            });
            }

        private List<ExternalResourceListOutputDto> GetQuesListOutputDto(List<ExternalResource> quesList)
            {
            return quesList.Select(q => new ExternalResourceListOutputDto()
                {
                id = q.ID,
                title = q.Title,
                type = q.Type,
                url = q.URL,
                desc = q.Description
                }).ToList();
            }
        }
    }