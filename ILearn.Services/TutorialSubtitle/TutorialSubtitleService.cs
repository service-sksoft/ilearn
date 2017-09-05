namespace ILearn.Services.TutorialSubtitle
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;

    public class TutorialSubtitleService : ITutorialSubtitleService
        {
        private readonly IEntityBaseRepository<TutorialSubtitle> tutorialSubtitleRepository;
        private readonly IUnitOfWork unitOfWork;

        public TutorialSubtitleService(IEntityBaseRepository<TutorialSubtitle> tutorialSubtitleRepository, IUnitOfWork unitOfWork)
            {
            this.tutorialSubtitleRepository = tutorialSubtitleRepository;
            this.unitOfWork = unitOfWork;
            }

        public List<TutorialSubtitle> GetAll()
            {
            return this.tutorialSubtitleRepository.GetAll().ToList();
            }

        public List<TutorialSubtitle> GetByParentID(int ParentID)
            {
            return this.tutorialSubtitleRepository.FindBy(x => x.ParentID == ParentID).ToList();
            }

        

        public TutorialSubtitle GetByID(int ID)
            {
            return this.tutorialSubtitleRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            }
        public bool Save(SaveTutorialSubtitleInputDto inputDTO)
            {
            TutorialSubtitle tt = this.tutorialSubtitleRepository.FindBy(x => x.ID == inputDTO.ID).FirstOrDefault();
            if (tt == null)
                {
                tt = new TutorialSubtitle();
                }

            tt.ParentID = inputDTO.ParentID;
            tt.Title = inputDTO.Title;
            tt.Seq = inputDTO.Seq;

            if (inputDTO.ID == 0)
                this.tutorialSubtitleRepository.Add(tt);
            else
                this.tutorialSubtitleRepository.Update(tt);

            this.unitOfWork.Commit();

            return true;
            }

        public bool Delete(int ID)
            {
            TutorialSubtitle tt = this.tutorialSubtitleRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            if (tt != null)
                {
                this.tutorialSubtitleRepository.Delete(tt);
                this.unitOfWork.Commit();
                return true;
                }
            return false;
            }
        }
    }
