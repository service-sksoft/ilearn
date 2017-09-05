
namespace ILearn.Services.TutorialTitle.Dtos
    {
    public class SaveTutorialTitleInputDto
        {
        public int ID { get; set; }
        public int TopicID { get; set; }
        public string Title { get; set; }
        public int Seq { get; set; }
        }
    }