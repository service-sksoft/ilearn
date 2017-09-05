namespace ILearn.Services.TutorialContent
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface ITutorialContentService
        {
        List<TutorialContent> GetAll();
        List<TutorialContent> GetByParentID(int ParentID);
        TutorialContent GetByID(int ID);
        bool Save(SaveTutorialContentInputDto inputDTO);
        bool Delete(int ID);
        }
    }
