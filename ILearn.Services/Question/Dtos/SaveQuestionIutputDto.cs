namespace ILearn.Services.QuesList.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class SaveQuestionIutputDto
        {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Language { get; set; }
        public int Type { get; set; }
        }
    }