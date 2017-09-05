namespace ILearn.Services.MultipleChoice
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;
    using System;

    public class MultipleChoiceService : IMultipleChoiceService
        {


        private readonly IEntityBaseRepository<MultipleChoice> multipleChoiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public MultipleChoiceService(IEntityBaseRepository<MultipleChoice> multipleChoiceRepository, IUnitOfWork unitOfWork)
            {
            this.multipleChoiceRepository = multipleChoiceRepository;
            this.unitOfWork = unitOfWork;
            }

        public bool Delete(int ID)
            {
            MultipleChoice mc = this.multipleChoiceRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            if (mc != null)
                {
                this.multipleChoiceRepository.Delete(mc);
                this.unitOfWork.Commit();
                return true;
                }
            return false;
            }

        public List<MultipleChoice> GetByTopicID(int TopicID)
            {
            return this.multipleChoiceRepository.FindBy(x => x.Language == TopicID).ToList();
            }
        public List<MultipleChoice> GetAll()
            {
            return this.multipleChoiceRepository.GetAll().ToList();
            }
        
        public MultipleChoice GetByID(int ID)
            {
            return this.multipleChoiceRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            }

        public bool SaveMultipleChoice(SaveMultipleChoiceInputDto inputDTO)
            {
            MultipleChoice mc = null;
            if (inputDTO.ID > 0)
                {
                mc = this.multipleChoiceRepository.FindBy(x => x.ID == inputDTO.ID).FirstOrDefault();
                }

            if (mc == null)
                {
                mc = new MultipleChoice();
                }

            mc.Language = inputDTO.Language;
            mc.Question = inputDTO.Question;
            mc.Option1 = inputDTO.Option1;
            mc.Option2 = inputDTO.Option2;
            mc.Option3 = inputDTO.Option3;
            mc.Option4 = inputDTO.Option4;
            mc.Option5 = inputDTO.Option5;
            mc.Answer = inputDTO.Answer;

            if (inputDTO.ID == 0)
                this.multipleChoiceRepository.Add(mc);
            else
                this.multipleChoiceRepository.Update(mc);

            this.unitOfWork.Commit();

            return true;
            }

        }
    }
