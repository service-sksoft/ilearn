namespace ILearn.Entities
    {
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TutorialContent")]
    public class TutorialContent : IBaseEntity
        {

        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Title { get; set; }

        public byte[] Description { get; set; }

        public int DescriptionLength { get; set; }

        public int Type { get; set; }

        public int Seq { get; set; }

        public string DescriptionString = "";
        }
    }