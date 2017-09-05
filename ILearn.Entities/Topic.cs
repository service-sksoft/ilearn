namespace ILearn.Entities
    {
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Topic")]
    public class Topic : IBaseEntity
        {

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public string TagLine { get; set; }

        public int ID { get; set; }

        public string Description { get; set; }
        }
    }