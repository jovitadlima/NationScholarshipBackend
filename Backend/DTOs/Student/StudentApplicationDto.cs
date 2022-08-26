using System;

namespace Backend.DTOs.Student
{
    public class StudentApplicationDto
    {
        public int AadharNumber { get; set; }
        public string Community { get; set; }
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
        public decimal PreviousClassPercentage { get; set; }
        public string RollNo10 { get; set; }
        public string BoardName10 { get; set; }
        public int PassingYear10 { get; set; }
        public decimal Percentage10 { get; set; }
        public string RollNo12 { get; set; }
        public string BoardName12 { get; set; }
        public int PassingYear12 { get; set; }
        public decimal Percentage12 { get; set; }
        public int AddmissionFee { get; set; }
        public int TutionFee { get; set; }
        public int OtherFee { get; set; }
        public bool IsDisabled { get; set; }
        public string TypeOfDisability { get; set; }
        public decimal PercentageDisability { get; set; }
        public string MartialStatus { get; set; }
        public string ParentProfession { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string HouseNumber { get; set; }
        public string StreetNumber { get; set; }
        public int Pincode { get; set; }
        public int MyProperty { get; set; }
        public string CertificateUrl { get; set; }
        public int StudentId { get; set; }
        public int SchemeId { get; set; }
    }
}
