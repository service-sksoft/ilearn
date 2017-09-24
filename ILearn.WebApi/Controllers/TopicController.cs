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
    using Services.MultipleChoice;
    using Services.QuesList;
    using Services.TutorialContent;
    using Services.TutorialSubtitle;
    using Services.TutorialTitle;

    [RoutePrefix("api/topic")]
    public class TopicController : ApiControllerBase
        {
        private readonly ITopicService topicService;
        private readonly IExternalResourceService externalResourceService;
        private readonly IMultipleChoiceService multipleChoiceService;
        private readonly IQuesListService quesListService;
        private readonly ITutorialContentService tutorialContentService;
        private readonly ITutorialSubtitleService tutorialsubtitleService;
        private readonly ITutorialTitleService tutorialTitleService;
        private HttpResponseMessage response;

        public TopicController(
            ITopicService topicService,
            IExternalResourceService externalResourceService,
            IMultipleChoiceService multipleChoiceService,
            IQuesListService quesListService,
            ITutorialContentService tutorialContentService,
            ITutorialSubtitleService tutorialsubtitleService,
            ITutorialTitleService tutorialTitleService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
            {
            this.topicService = topicService;
            this.externalResourceService = externalResourceService;
            this.multipleChoiceService = multipleChoiceService;
            this.quesListService = quesListService;
            this.tutorialContentService = tutorialContentService;
            this.tutorialsubtitleService = tutorialsubtitleService;
            this.tutorialTitleService = tutorialTitleService;

            if (GlobalList.Topics.Count() == 0)
                LoadGlobalData();
            }

        [Route("reload")]
        [HttpGet]
        public HttpResponseMessage ReloadData(HttpRequestMessage request)
            {
            LoadGlobalData();
            return response = request.CreateResponse(HttpStatusCode.OK, true);
            }

        [Route("alltopics")]
        [HttpGet]
        public HttpResponseMessage GetAllForList(HttpRequestMessage request)
            {
            return CreateHttpResponse(request, () =>
            {
                if (GlobalList.Topics.Count > 0)
                    {
                    var result = this.GetTopicOutputDto(GlobalList.Topics);
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }

                return response;
            });
            }

        [Route("topics")]
        [HttpGet]
        public HttpResponseMessage GetAllTopics(HttpRequestMessage request)
            {
            return CreateHttpResponse(request, () =>
            {
                var alltopics = this.topicService.GetAll().ToList();

                if (alltopics.Count > 0)
                    {
                    var result = this.GetTopicOutputDto(alltopics);
                    response = request.CreateResponse(HttpStatusCode.OK, new { result });
                    }
                else
                    {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, string.Empty);
                    }

                return response;
            });
            }

        [Route("topics/{id}")]
        [HttpGet]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
            {
            return CreateHttpResponse(request, () =>
            {
                var results = this.topicService.GetByID(id);

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

        private List<TopicOutputDto> GetTopicOutputDto(List<Topic> topicList)
            {
            return topicList.Select(q => new TopicOutputDto()
                {
                ID = q.ID,
                Name = q.Name,
                DisplayName = q.DisplayName,
                URL = q.URL,
                Description = q.Description,
                Image = Cmn.getTopicImageURL(q.ID),
                InterviewCount = GlobalList.Questions.Where(x => x.Language == q.ID).Count(),
                MultipleChoiceCount = GlobalList.MultipleChoice.Where(x => x.Language == q.ID).Count(),
                TutorialCount = GlobalList.TutorialTitle.Where(x => x.TopicID == q.ID).Count(),
                ExternalResourceCount = GlobalList.ExternalResources.Where(x => x.TopicID == q.ID).Count()
                }).ToList();
            }

        [Route("SaveTopic")]
        [HttpPost]
        public HttpResponseMessage SaveTopic(HttpRequestMessage request, SaveTopicInputDto inputDTO)
            {
            return CreateHttpResponse(request, () =>
            {
                var result = this.topicService.Save(inputDTO);

                response = request.CreateResponse(HttpStatusCode.OK, new { result });

                return response;
            });
            }

        [Route("searchquestion")]
        [HttpGet]
        public HttpResponseMessage SearchQuestion(HttpRequestMessage request, [FromUri] string term)
            {
            term = term.ToUpper();

            return CreateHttpResponse(request, () =>
            {
                var result = GlobalList.Questions.Where(m => m.Question.ToUpper().IndexOf(term) > -1).Take(25).ToList().Select(q => new
                    {
                    id = q.ID,
                    q = q.Question,
                    a = q.AnswerString,
                    t = q.Language
                    }).ToList();

                response = request.CreateResponse(HttpStatusCode.OK, new { result });
                return response;
            });
            }


        private void LoadGlobalData()
            {
            GlobalList.Topics = topicService.GetAll();
            GlobalList.Questions = quesListService.GetAll();
            GlobalList.MultipleChoice = multipleChoiceService.GetAll();
            GlobalList.ExternalResources = externalResourceService.GetAll();
            GlobalList.TutorialTitle = tutorialTitleService.GetAll();
            GlobalList.TutorialSubtitle = tutorialsubtitleService.GetAll();
            GlobalList.TutorialContent = tutorialContentService.GetAll();

            GlobalList.TutorialContent.ToList().ForEach(c => c.DescriptionString = Cmn.GetUnCompressed(c.Description, c.DescriptionLength));
            GlobalList.Questions.ToList().ForEach(c => c.AnswerString = Cmn.GetUnCompressed(c.Answer, c.AnswerLength));

            }
        }
    }