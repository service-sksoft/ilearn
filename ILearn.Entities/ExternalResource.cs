namespace ILearn.Entities
    {
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ExternalResource")]
    public class ExternalResource : IBaseEntity
        {

        public string Title { get; set; }

        public string URL { get; set; }

        public int ID { get; set; }

        public int TopicID { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        }
    }