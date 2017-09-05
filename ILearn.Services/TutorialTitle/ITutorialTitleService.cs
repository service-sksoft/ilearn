namespace ILearn.Services.TutorialTitle
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface ITutorialTitleService
        {
        List<TutorialTitle> GetAll();

        List<TutorialTitle> GetByParentID(int TopicID);
        TutorialTitle GetByID(int ID);
        bool Save(SaveTutorialTitleInputDto inputDTO);

        bool Delete(int ID);
        }
    }
