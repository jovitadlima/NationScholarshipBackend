using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class InstituteDocument
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }

        //reference key
        public int ApplicationId { get; set; }
        public ScholarshipApplication ScholarshipApplication { get; set; }
    }
}
