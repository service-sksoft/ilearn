namespace ILearn.Services.MultipleChoice
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface IMultipleChoiceService
        {

        List<MultipleChoice> GetAll();
        List<MultipleChoice> GetByTopicID(int TopicID);

        MultipleChoice GetByID(int ID);

        bool SaveMultipleChoice(SaveMultipleChoiceInputDto inputDTO);

        bool Delete(int ID);
        }
    }
