namespace ILearn.Services.QuesList
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;
    using Common;
    using System;

    public class QuesListService : IQuesListService
        {

        private readonly IEntityBaseRepository<QuesList> questionRepository;
        private readonly IUnitOfWork unitOfWork;

        public QuesListService(IEntityBaseRepository<QuesList> questionRepository, IUnitOfWork unitOfWork)
            {
            this.questionRepository = questionRepository;
            this.unitOfWork = unitOfWork;
            }

        public bool Delete(int ID)
            {
            QuesList ques = this.questionRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            if (ques != null)
                {
                this.questionRepository.Delete(ques);
                this.unitOfWork.Commit();
                return true;
                }
            return false;
            }


        public List<QuesList> GetCompleteList()
            {
            return this.questionRepository.GetAll().ToList();
            }

        public List<QuesList> GetAll()
            {
            return this.questionRepository.GetAll().ToList();
            }

        public List<QuesList> GetByTopicID(int TopicID)
            {
            return this.questionRepository.FindBy(x => x.Language == TopicID).ToList();
            }



        public QuesList GetByID(int ID)
            {
            return this.questionRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            }

        public bool Save(SaveQuestionIutputDto inputQuestion)
            {
            QuesList ques = null;
            if (inputQuestion.ID > 0)
                {
                ques = this.questionRepository.FindBy(x => x.ID == inputQuestion.ID).FirstOrDefault();
                }

            if (ques == null)
                {
                ques = new QuesList();
                }

            ques.Question = inputQuestion.Question;
            ques.Answer = Cmn.GetCompressed(inputQuestion.Answer);
            ques.AnswerLength = inputQuestion.Answer.Length;
            ques.Language = inputQuestion.Language;
            ques.Type = inputQuestion.Type;
            ques.Reviewed = 1;

            if (inputQuestion.ID == 0)
                {
                this.questionRepository.Add(ques);
                }
            else
                {
                this.questionRepository.Update(ques);
                }

            this.unitOfWork.Commit();
            return true;
            }
        }
    }
