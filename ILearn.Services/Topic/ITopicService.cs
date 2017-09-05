namespace ILearn.Services.Topic
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface ITopicService
        {
        List<Topic> GetAll();
        Topic GetByID(int ID);
        bool Save(SaveTopicInputDto inputDTO);
        }
    }
