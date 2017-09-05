namespace ILearn.Services.ExternalResource
    {
    using System.Collections.Generic;
    using System.Linq;

    using ILearn.Data.Infrastructure;
    using ILearn.Data.Repositories;
    using ILearn.Entities;
    using Dtos;
    using System;

    public class ExternalResourceService : IExternalResourceService
        {


        private readonly IEntityBaseRepository<ExternalResource> externalResourceRepository;
        private readonly IUnitOfWork unitOfWork;

        public ExternalResourceService(IEntityBaseRepository<ExternalResource> externalResourceRepository, IUnitOfWork unitOfWork)
            {
            this.externalResourceRepository = externalResourceRepository;
            this.unitOfWork = unitOfWork;
            }

        public bool DeleteReference(int ID)
            {
            ExternalResource er = this.externalResourceRepository.FindBy(x => x.ID == ID).FirstOrDefault();
            if (er != null)
                {
                this.externalResourceRepository.Delete(er);
                this.unitOfWork.Commit();
                return true;
                }
            return false;
            }

        public List<ExternalResource> GetByTopicID(int TopicID)
            {
            return this.externalResourceRepository.FindBy(x => x.TopicID == TopicID).ToList();
            }

        public List<ExternalResource> GetAll()
            {
            return this.externalResourceRepository.GetAll().ToList();
            }


        public bool SaveReference(SaveExternalResourceInputDto inputDTO)
            {
            ExternalResource er = null;
            if (inputDTO.ID > 0)
                {
                er = this.externalResourceRepository.FindBy(x => x.ID == inputDTO.ID).FirstOrDefault();
                }

            if (er == null)
                {
                er = new ExternalResource();
                er.ID = 0;
                }

            er.TopicID = inputDTO.TopicID;
            er.Title = inputDTO.Title;
            er.URL = inputDTO.URL;
            er.Type = inputDTO.Type;
            er.Description = inputDTO.Description;

            if (inputDTO.ID == 0)
                this.externalResourceRepository.Add(er);
            else
                this.externalResourceRepository.Update(er);

            this.unitOfWork.Commit();

            return true;
            }

        }
    }
