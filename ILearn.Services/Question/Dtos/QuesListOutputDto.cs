namespace ILearn.Services.QuesList.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class QuesListOutputDto
        {
        public int ID { get; set; }
        public string Question{ get; set; }
        public string Answer { get; set; }
        public int Topic { get; set; }
        }
    }