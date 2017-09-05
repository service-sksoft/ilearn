namespace ILearn.Services.ExternalResource.Dtos
    {
    using System.ComponentModel.DataAnnotations;

    public class ExternalResourceListOutputDto
        {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public int type { get; set; }

        public string desc { get; set; }
        }
    }