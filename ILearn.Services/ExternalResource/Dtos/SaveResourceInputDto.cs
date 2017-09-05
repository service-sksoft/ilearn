
namespace ILearn.Services.ExternalResource.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class SaveExternalResourceInputDto
        {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        }
    }