
namespace ILearn.Services.TutorialSubtitle.Dtos
    {
    public class SaveTutorialSubtitleInputDto
        {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; }
        public int Seq { get; set; }
        }
    }