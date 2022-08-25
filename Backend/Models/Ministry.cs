using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Ministry
    {
        [Key]
        public int MinistryId { get; set; }
        public string Name { get; set; }
        public string MinistryEmail { get; set; }
    }
}
