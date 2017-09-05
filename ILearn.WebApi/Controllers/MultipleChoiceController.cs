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
    using Services.MultipleChoice;
    using Services.MultipleChoice.Dtos;

    [RoutePrefix("api/multiplechoice")]
    public class MultipleChoiceController : ApiControllerBase
        {
        private readonly IMultipleChoiceService multipleChoiceService;
        private HttpResponseMessage response;

        public MultipleChoiceController(
            IMultipleChoiceService multipleChoiceService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.multipleChoiceService = multipleChoiceService;

            if (GlobalList.MultipleChoice.Count() == 0)
                GlobalList.MultipleChoice = multipleChoiceService.GetAll();

            }

        [Route("questions/{topicid}/{pageNo}")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int topicid, int pageNo)
            {
            return CreateHttpResponse(request, () =>
            {
                var allQues = GlobalList.MultipleChoice.Where(x => x.Language == topicid).ToList();
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

        [Route("questiondetail/{ID}")]
        [HttpGet]
        public HttpResponseMessage GetQuestionDetail(HttpRequestMessage request, int ID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.multipleChoiceService.GetByID(ID);

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

        [Route("{TopicID}")]
        [HttpGet]
        public HttpResponseMessage GetMultipleChoiceByTopicID(HttpRequestMessage request, int TopicID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.multipleChoiceService.GetByTopicID(TopicID);

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

        [Route("questionsonly/{TopicID}")]
        [HttpGet]
        public HttpResponseMessage GetQuestionsOnlyByTopicID(HttpRequestMessage request, int TopicID)
            {
            return CreateHttpResponse(request, () =>
            {
                var resp = this.multipleChoiceService.GetByTopicID(TopicID);

                if (resp != null)
                    {
                    var result = this.GetOnlyQuestions(resp);
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }

                return response;
            });
            }


        [HttpPost]
        public HttpResponseMessage SaveQuestion(HttpRequestMessage request, SaveMultipleChoiceInputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var results = this.multipleChoiceService.SaveMultipleChoice(inputDTO);
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
                var result = this.multipleChoiceService.Delete(ID);
                response = request.CreateResponse(HttpStatusCode.OK, new { result });
                return response;
            });
            }

        private List<MultipleChoiceOnlyQuestionOutputDto> GetOnlyQuestions(List<MultipleChoice> questions)
            {
            return questions.Select(q => new MultipleChoiceOnlyQuestionOutputDto()
                {
                ID = q.ID,
                Question = q.Question,
                Language = q.Language
                }).ToList();

            }

        private List<MultipleChoiceOutputDto> GetQuesListOutputDto(List<MultipleChoice> quesList)
            {
            return quesList.Select(q => new MultipleChoiceOutputDto()
                {
                q=q.Question,
                id = q.ID,
                tID = q.Language,
                o1 = q.Option1,
                o2 = q.Option2,
                o3 = q.Option3,
                o4 = q.Option4,
                o5 = q.Option5,
                a = q.Answer
                }).ToList();
            }
        }


    }