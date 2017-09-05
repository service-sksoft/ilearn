namespace ILearn.Services.TutorialContent.Dtos
    {
    public class TutorialContentOutputDto
        {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Seq { get; set; }
        }
    }