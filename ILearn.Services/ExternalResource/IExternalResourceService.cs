namespace ILearn.Services.ExternalResource
    {
    using System.Collections.Generic;

    using ILearn.Entities;
    using Dtos;

    public interface IExternalResourceService
        {

        List<ExternalResource> GetAll();
        List<ExternalResource> GetByTopicID(int TopicID);

        bool SaveReference(SaveExternalResourceInputDto inputDTO);

        bool DeleteReference(int ID);
        }
    }
