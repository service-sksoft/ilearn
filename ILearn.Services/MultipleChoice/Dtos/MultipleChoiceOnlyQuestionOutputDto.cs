namespace ILearn.Services.MultipleChoice.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class MultipleChoiceOnlyQuestionOutputDto
        {
        public int Language { get; set; }

        public string Question { get; set; }
        
        public int ID { get; set; }
        }
    }