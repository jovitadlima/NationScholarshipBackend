using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Institute
    {
        [Key]
        public int InstituteId { get; set; }
        public string InstituteCategory { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string InstituteName { get; set; }
        public string InstituteCode { get; set; }
        public string DiseCode { get; set; }
        public string Location { get; set; }
        public string InstituteType { get; set; }
        public string AffliatedUniversityState { get; set; }
        public string AffliatedUniversityName { get; set; }
        public string YearOfAddmission { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressDistrict { get; set; }
        public int AddressPincode { get; set; }
        public string PrincipalName { get; set; }
        public string MobileNumber { get; set; }
        public string Telephone { get; set; }
        public bool ApprovedByOfficer { get; set; } = false;
        public bool ApprovedByMinistry { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public string RegistrationCertificate { get; set; } = string.Empty;
        public string UniversityAffliationCertificate { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // navigation property
        public ICollection<Student> Student { get; set; }
    }
}
