using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ScholarshipScheme
    {
        [Key]
        public int SchemeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ScholarshipApplication> ScholarshipApplication { get; set; }
    }
}
