namespace ILearn.Services.ExternalResource.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class ExternalResourceOutputDto
        {
        public int ID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        }
    }