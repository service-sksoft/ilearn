namespace ILearn.WebApi.Controllers
    {
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.WebApi.Infrastructure.Core;
    using Services.QuesList;
    using Services.Topic;
    using Services.QuesList.Dtos;
    using Entities;
    using System.Collections.Generic;
    using Services.Common;

    [RoutePrefix("api/queslist")]
    public class QuesListController : ApiControllerBase
        {
        private readonly IQuesListService quesListService;
        private readonly ITopicService topicService;

        private HttpResponseMessage response;

        public QuesListController(
            IQuesListService quesListService,
            ITopicService topicService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.quesListService = quesListService;
            this.topicService = topicService;

            if (GlobalList.Questions.Count() == 0)
                GlobalList.Questions = quesListService.GetAll();
            }

        [Route("questions/{topicid}/{pageNo}")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, int topicid, int pageNo)
            {
            return CreateHttpResponse(request, () =>
            {
                var allQues = GlobalList.Questions.Where(x => x.Language == topicid).ToList();
                var quesToReturn = allQues.Skip((pageNo-1) * Cmn.MaxQuestionPerPage).Take(Cmn.MaxQuestionPerPage).ToList();

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

        [Route("question/{id}")]
        [HttpGet]
        public HttpResponseMessage GetQuestion(HttpRequestMessage request, int id)
            {
            return CreateHttpResponse(request, () =>
            {
                var ques = this.quesListService.GetByID(id);
                ques.AnswerString = Cmn.GetUnCompressed(ques.Answer, ques.AnswerLength);
                if (ques != null)
                    {
                    response = request.CreateResponse(HttpStatusCode.OK, new { ques });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, id.ToString());
                    }
                return response;
            });
            }

        [Route("onlyquestions/{topicid}")]
        [HttpGet]
        public HttpResponseMessage GetOnlyQuestions(HttpRequestMessage request, int topicid)
            {
            return CreateHttpResponse(request, () =>
            {
                var allQuestions = this.quesListService.GetByTopicID(topicid);

                if (allQuestions != null)
                    {
                    var result = this.GetQuesListOnlyQuestionOutputDto(allQuestions);
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, topicid.ToString());
                    }

                return response;
            });
            }

        [Route("question")]
        [HttpPost]
        public HttpResponseMessage SaveQuestions(HttpRequestMessage request, SaveQuestionIutputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.quesListService.Save(inputDTO);

                if (inputDTO != null)
                    {
                    response = request.CreateResponse(HttpStatusCode.OK, new { inputDTO });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, inputDTO.ID.ToString());
                    }

                return response;
            });
            }

        [Route("deletequestion/{ID}")]
        [HttpPost]
        public HttpResponseMessage DeleteQuestion(HttpRequestMessage request, int ID)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.quesListService.Delete(ID);
                response = request.CreateResponse(HttpStatusCode.OK, new { result });
                return response;
            });
            }


        private List<QuesListDisplayOutputDto> GetQuesListOutputDto(List<QuesList> quesList)
            {
            return quesList.Select(q => new QuesListDisplayOutputDto()
                {
                id = q.ID,
                a = Cmn.GetUnCompressed(q.Answer, q.AnswerLength),
                q = q.Question,
                t = (int)QuesType.SUBJECTIVE
                }).ToList();
            }


        private List<QuesListOutputDto> GetQuesListOnlyQuestionOutputDto(List<QuesList> quesList)
            {
            return quesList.Select(q => new QuesListOutputDto()
                {
                ID = q.ID,
                Question = q.Question,
                Topic = (int)QuesType.SUBJECTIVE
                }).ToList();
            }
        }
    }
