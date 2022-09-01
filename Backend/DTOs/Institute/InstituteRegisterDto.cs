using System;

namespace Backend.DTOs.Institute
{
    public class InstituteRegisterDto
    {
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

        public string Password { get; set; }
        public string Password2 { get; set; }

        public string RegistrationCertificate { get; set; } 
        public string UniversityAffliationCertificate { get; set; } 
    }
}
