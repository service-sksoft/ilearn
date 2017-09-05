namespace ILearn.Services.MultipleChoice.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class MultipleChoiceOutputDto
        {
        public int tID { get; set; }

        public string q { get; set; }

        public int id { get; set; }

        public int a { get; set; }

        public string o1 { get; set; }

        public string o2 { get; set; }

        public string o3 { get; set; }

        public string o4 { get; set; }

        public string o5 { get; set; }

        }
    }