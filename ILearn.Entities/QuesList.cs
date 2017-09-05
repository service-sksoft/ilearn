namespace ILearn.Entities
    {
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("QuesList")]
    public class QuesList : IBaseEntity
    {
        public string Message = "";
        public string AnswerString = "";
        public string URL = "";

        public int Language { get; set; }

        public int AnswerLength { get; set; }

        public byte[] Answer { get; set; }

        public string Question { get; set; }
        
        public int ID { get; set; }

        public int Reviewed { get; set; }

        public string Tags { get; set; }

        public int Type { get; set; }
        
        }
}