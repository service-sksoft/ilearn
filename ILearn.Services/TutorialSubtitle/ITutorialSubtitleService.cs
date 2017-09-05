namespace ILearn.Services.TutorialSubtitle
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface ITutorialSubtitleService
        {
        List<TutorialSubtitle> GetAll();

        List<TutorialSubtitle> GetByParentID(int ParentID);
        TutorialSubtitle GetByID(int ID);
        bool Save(SaveTutorialSubtitleInputDto inputDTO);

        bool Delete(int ID);
        }
    }
