namespace ILearn.Services.QuesList.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class QuesListWithNoAnswerOutputDto
        {
        public int ID { get; set; }
        public string Question { get; set; }
        public int Topic { get; set; }
        }
    }