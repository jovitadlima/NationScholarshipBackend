using System;

namespace Backend.DTOs.Student
{
    public class StudentRegisterDto
    {
        public string StudentName { get; set; }
        public string DateOfBirth { get; set; }
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
    }
}
