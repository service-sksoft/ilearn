namespace ILearn.Entities
    {
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MultipleChoice")]
    public class MultipleChoice : IBaseEntity
        {

        public int Language { get; set; }

        public string Question { get; set; }

        public int ID { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string Option5 { get; set; }
        public int Answer { get; set; }

        }
    }