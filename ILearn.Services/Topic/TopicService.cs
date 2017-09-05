namespace ILearn.Services.Topic
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;

    public class TopicService : ITopicService
        {
        private readonly IEntityBaseRepository<Topic> topicRepository;
        private readonly IUnitOfWork unitOfWork;

        public TopicService(IEntityBaseRepository<Topic> topicRepository, IUnitOfWork unitOfWork)
            {
            this.topicRepository = topicRepository;
            this.unitOfWork = unitOfWork;
            }

        public List<Topic> GetAll()
            {
            return this.topicRepository.GetAll().ToList();
            }

        public Topic GetByID(int ID)
            {
            return this.topicRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            }
        public bool Save(SaveTopicInputDto inputDTO)
            {
            Topic topic = this.topicRepository.FindBy(x => x.ID == inputDTO.ID).FirstOrDefault();
            if (topic == null)
                {
                topic = new Topic();
                topic.ID = this.topicRepository.GetAll().OrderByDescending(item => item.ID).FirstOrDefault().ID + 1;
                }

            topic.Name = inputDTO.Name;
            topic.DisplayName = inputDTO.DisplayName;
            topic.Description = inputDTO.Description;

            if (inputDTO.ID == 0)
                this.topicRepository.Add(topic);
            else
                this.topicRepository.Update(topic);

            this.unitOfWork.Commit();

            return true;
            }
        }
    }
