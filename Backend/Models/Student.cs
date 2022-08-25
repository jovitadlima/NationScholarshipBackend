using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime MyProperty { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string InstituteCode { get; set; }
        public string PhoneNo { get; set; }
        public string StateOfDomicile { get; set; }
        public string District { get; set; }
        public int AadharNumber { get; set; }
        public string BankIfscCode { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }

        // navigation property
        public ScholarshipApplication ScholarshipApplication { get; set; }
    }
}

