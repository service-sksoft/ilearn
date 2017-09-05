namespace ILearn.Services.Topic.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class TopicOutputDto
        {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string URL { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }

        public int InterviewCount { get; set; }
        public int MultipleChoiceCount { get; set; }
        public int TutorialCount { get; set; }
        public int ExternalResourceCount { get; set; }

        }
    }