namespace ILearn.Services.QuesList.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class QuesListDisplayOutputDto
        {
        public int id { get; set; }
        public string q{ get; set; }
        public string a { get; set; }
        public int t { get; set; }
        }
    }