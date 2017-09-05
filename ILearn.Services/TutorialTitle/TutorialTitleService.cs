namespace ILearn.Services.TutorialTitle
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;

    public class TutorialTitleService : ITutorialTitleService
        {
        private readonly IEntityBaseRepository<TutorialTitle> tutorialTitleRepository;
        private readonly IUnitOfWork unitOfWork;

        public TutorialTitleService(IEntityBaseRepository<TutorialTitle> tutorialTitleRepository, IUnitOfWork unitOfWork)
            {
            this.tutorialTitleRepository = tutorialTitleRepository;
            this.unitOfWork = unitOfWork;
            }

        public List<TutorialTitle> GetAll()
            {
            return this.tutorialTitleRepository.GetAll().ToList();
            }

        public List<TutorialTitle> GetByParentID(int TopicID)
            {
            return this.tutorialTitleRepository.FindBy(x => x.TopicID == TopicID).ToList();
            }

        

        public TutorialTitle GetByID(int ID)
            {
            return this.tutorialTitleRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            }
        public bool Save(SaveTutorialTitleInputDto inputDTO)
            {
            TutorialTitle tt = this.tutorialTitleRepository.FindBy(x => x.ID == inputDTO.ID).FirstOrDefault();
            if (tt == null)
                {
                tt = new TutorialTitle();
                }

            tt.TopicID = inputDTO.TopicID;
            tt.Title = inputDTO.Title;
            tt.Seq = inputDTO.Seq;

            if (inputDTO.ID == 0)
                this.tutorialTitleRepository.Add(tt);
            else
                this.tutorialTitleRepository.Update(tt);

            this.unitOfWork.Commit();

            return true;
            }

        public bool Delete(int ID)
            {
            TutorialTitle tt = this.tutorialTitleRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            if (tt != null)
                {
                this.tutorialTitleRepository.Delete(tt);
                this.unitOfWork.Commit();
                return true;
                }
            return false;
            }


        }
    }
