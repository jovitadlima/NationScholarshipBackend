using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ScholarshipApplication
    {
        [Key]
        public int ApplicationId { get; set; }
        public string AadharNumber { get; set; }
        public string Community { get; set; }
        public string Religion { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int AnnualIncome { get; set; }
        public string InstituteName { get; set; }
        public string PresentCourse { get; set; }
        public int PresentCourseYear { get; set; }
        public string ModeOfStudy { get; set; }
        public string ClassStartDate { get; set; }
        public string UniversityBoardName { get; set; }
        public string PreviousCourse { get; set; }
        public int PreviousPassingYear { get; set; }
        public int PreviousClassPercentage { get; set; }
        public string RollNo10 { get; set; }
        public string BoardName10 { get; set; }
        public int PassingYear10 { get; set; }
        public int Percentage10 { get; set; }
        public string RollNo12 { get; set; }
        public string BoardName12 { get; set; }
        public int PassingYear12 { get; set; }
        public int Percentage12 { get; set; }
        public int AddmissionFee { get; set; }
        public int TutionFee { get; set; }
        public int OtherFee { get; set; }
        public string IsDisabled { get; set; }
        public string TypeOfDisability { get; set; }
        public int PercentageDisability { get; set; }
        public string MartialStatus { get; set; }
        public string ParentProfession { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string HouseNumber { get; set; }
        public string StreetNumber { get; set; }
        public int Pincode { get; set; }
        public bool ApprovedByInstitute { get; set; } = false;
        public bool ApprovedByOfficer { get; set; } = false;
        public bool ApprovedByMinistry { get; set; } = false;
        public string InstituteCode { get; set; }
        public bool IsRejected { get; set; } = false;

        public string DomicileCertificate { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string InstituteIdCard { get; set; } = string.Empty;
        public string CasteOrIncomeCertificate { get; set; } = string.Empty;
        public string PreviousYearMarksheet { get; set; } = string.Empty;
        public string FeeReceiptOfCurrentYear { get; set; } = string.Empty;
        public string BankPassBook { get; set; } = string.Empty;
        public string AadharCard { get; set; } = string.Empty;
        public string MarkSheet10 { get; set; } = string.Empty;
        public string MarkSheet12 { get; set; } = string.Empty;

        // navigation property
        [ForeignKey("ScholarshipScheme")]
        public int SchemeId { get; set; }
        public ScholarshipScheme ScholarshipScheme { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
