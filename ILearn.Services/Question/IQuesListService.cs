namespace ILearn.Services.QuesList
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface IQuesListService
        {

        List<QuesList> GetAll();
        
        List<QuesList> GetByTopicID(int TopicID);
        QuesList GetByID(int ID);
        bool Save(SaveQuestionIutputDto question);

        bool Delete(int ID);
        }
    }
