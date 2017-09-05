
namespace ILearn.Services.Topic.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class SaveTopicInputDto
        {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        }
    }