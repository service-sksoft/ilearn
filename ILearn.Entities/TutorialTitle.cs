namespace ILearn.Entities
    {
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TutorialTitle")]
    public class TutorialTitle : IBaseEntity
        {

        public int ID { get; set; }

        public int TopicID { get; set; }

        public string Title { get; set; }

        public int Seq { get; set; }
        }
    }