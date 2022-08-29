using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string InstituteCode { get; set; }
        public string PhoneNo { get; set; }
        public string StateOfDomicile { get; set; }
        public string District { get; set; }
        public string AadharNumber { get; set; }
        public string BankIfscCode { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // navigation property

        [ForeignKey("Institute")]
        public int InstituteId { get; set; }
        public Institute Institute { get; set; }
        public ScholarshipApplication ScholarshipApplication { get; set; }
    }
}

