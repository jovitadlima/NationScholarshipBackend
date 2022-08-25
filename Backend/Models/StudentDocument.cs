using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class StudentDocument
    {
        [Key]
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string DocUrl { get; set; }

        // navigation property
        public int ApplicationId { get; set; }
        public ScholarshipApplication ScholarshipApplication { get; set; }
    }
}
