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
        public int InstituteCategory { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string InstituteName { get; set; }
        public int InstituteCode { get; set; }
        public string DiseCode { get; set; }
        public string Location { get; set; }
        public string InstituteType { get; set; }
        public string AffliatedUniversityState { get; set; }
        public string AffliatedUniversityName { get; set; }
        public int YearOfAddmission { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressDistrict { get; set; }
        public int AddressPincode { get; set; }
        public int PrincipalName { get; set; }
        public string MobileNumber { get; set; }
        public string Telephone { get; set; }
        public bool ApprovedByOfficer { get; set; }
        public bool ApprovedByMinistery { get; set; }

        // navigation property
        public ICollection<InstituteDocument> InstituteDocument { get; set; }
    }
}
