namespace ILearn.Services.TutorialContent
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;
    using Common;

    public class TutorialContentService : ITutorialContentService
        {
        private readonly IEntityBaseRepository<TutorialContent> tutorialContentRepository;
        private readonly IUnitOfWork unitOfWork;

        public TutorialContentService(IEntityBaseRepository<TutorialContent> tutorialContentRepository, IUnitOfWork unitOfWork)
            {
            this.tutorialContentRepository = tutorialContentRepository;
            this.unitOfWork = unitOfWork;
            }

        public List<TutorialContent> GetAll()
            {
            return this.tutorialContentRepository.GetAll().ToList();
            }

        public TutorialContent GetByID(int ID)
            {
            return this.tutorialContentRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            }

        public List<TutorialContent> GetByParentID(int ParentID)
            {
            return this.tutorialContentRepository.FindBy(x => x.ParentID == ParentID).ToList();
            }

        
        public bool Save(SaveTutorialContentInputDto inputDTO)
            {
            TutorialContent tt = this.tutorialContentRepository.FindBy(x => x.ID == inputDTO.ID).FirstOrDefault();
            if (tt == null)
                {
                tt = new TutorialContent();
                }

            tt.ParentID = inputDTO.ParentID;
            tt.Description = Cmn.GetCompressed(inputDTO.Description);
            tt.DescriptionLength = inputDTO.Description.Length;
            tt.Title = inputDTO.Title;
            tt.Type = inputDTO.Type;
            tt.Seq = inputDTO.Seq;

            if (inputDTO.ID == 0)
                this.tutorialContentRepository.Add(tt);
            else
                this.tutorialContentRepository.Update(tt);

            this.unitOfWork.Commit();

            return true;
            }

        public bool Delete(int ID)
            {
            TutorialContent tt = this.tutorialContentRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            if (tt != null)
                {
                this.tutorialContentRepository.Delete(tt);
                this.unitOfWork.Commit();
                return true;
                }
            return false;
            }

        }
    }
