namespace ILearn.Entities
    {
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TutorialSubtitle")]
    public class TutorialSubtitle : IBaseEntity
        {

        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Title { get; set; }

        public int Seq { get; set; }
        }
    }