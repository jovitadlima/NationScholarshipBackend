using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs.Student;
using Backend.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ScholarshipDbContext _context;

        public StudentController(ScholarshipDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _context.Students.ToList();

                if (!students.Any()) return NotFound("No Student Present");

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null) return NotFound();

            return Ok(student);

        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] StudentRegisterDto studentRegisterDto)
        {
            try
            {
                var student = new Student()
                {
                    StudentName = studentRegisterDto.StudentName,
                    DateOfBirth = Convert.ToDateTime(studentRegisterDto.DateOfBirth),
                    Gender = studentRegisterDto.Gender,
                    Email = studentRegisterDto.Email,
                    InstituteCode = studentRegisterDto.InstituteCode,
                    PhoneNo = studentRegisterDto.PhoneNo,
                    StateOfDomicile = studentRegisterDto.StateOfDomicile,
                    District = studentRegisterDto.District,
                    AadharNumber = studentRegisterDto.AadharNumber,
                    BankIfscCode = studentRegisterDto.BankIfscCode,
                    BankAccountNumber = studentRegisterDto.BankAccountNumber,
                    BankName = studentRegisterDto.BankName,
                };

                var institute = _context.Institutes.Where(institute => institute.InstituteCode == student.InstituteCode).FirstOrDefault();
                student.InstituteId = institute.InstituteId;

                _context.Students.Add(student);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to create student");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var student = _context.Students.Find(id);

                _context.Students.Remove(student);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to create student");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteApplication/{id}")]
        public IActionResult DeleteApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                _context.ScholarshipApplications.Remove(application);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to create student");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateApplication")]
        public IActionResult CreateApplication([FromBody] StudentApplicationDto studentApplicationDto)
        {
            try
            {
                var studentApplication = new ScholarshipApplication()
                {
                    AadharNumber = studentApplicationDto.AadharNumber,
                    Community = studentApplicationDto.Community,
                    FatherName = studentApplicationDto.FatherName,
                    MotherName = studentApplicationDto.MotherName,
                    AnnualIncome = studentApplicationDto.AnnualIncome,
                    InstituteName = studentApplicationDto.InstituteName,
                    PresentCourse = studentApplicationDto.PresentCourse,
                    PresentCourseYear = studentApplicationDto.PresentCourseYear,
                    ModeOfStudy = studentApplicationDto.ModeOfStudy,
                    ClassStartDate = Convert.ToDateTime(studentApplicationDto.ClassStartDate),
                    UniversityBoardName = studentApplicationDto.UniversityBoardName,
                    PreviousCourse = studentApplicationDto.PreviousCourse,
                    PreviousPassingYear = studentApplicationDto.PreviousPassingYear,
                    PreviousClassPercentage = studentApplicationDto.PreviousClassPercentage,
                    RollNo10 = studentApplicationDto.RollNo10,
                    BoardName10 = studentApplicationDto.BoardName10,
                    PassingYear10 = studentApplicationDto.PassingYear10,
                    Percentage10 = studentApplicationDto.Percentage10,
                    RollNo12 = studentApplicationDto.RollNo12,
                    BoardName12 = studentApplicationDto.BoardName12,
                    PassingYear12 = studentApplicationDto.PassingYear12,
                    Percentage12 = studentApplicationDto.PassingYear12,
                    AddmissionFee = studentApplicationDto.AddmissionFee,
                    TutionFee = studentApplicationDto.TutionFee,
                    OtherFee = studentApplicationDto.OtherFee,
                    IsDisabled = studentApplicationDto.IsDisabled,
                    TypeOfDisability = studentApplicationDto.TypeOfDisability,
                    PercentageDisability = studentApplicationDto.PercentageDisability,
                    MartialStatus = studentApplicationDto.MartialStatus,
                    ParentProfession = studentApplicationDto.ParentProfession,
                    State = studentApplicationDto.State,
                    District = studentApplicationDto.District,
                    Block = studentApplicationDto.Block,
                    HouseNumber = studentApplicationDto.HouseNumber,
                    StreetNumber = studentApplicationDto.StreetNumber,
                    Pincode = studentApplicationDto.Pincode,
                    CertificateUrl = studentApplicationDto.CertificateUrl,
                    StudentId = studentApplicationDto.StudentId,
                    SchemeId = studentApplicationDto.SchemeId,
                    InstituteCode = studentApplicationDto.InstituteCode
                };

                _context.ScholarshipApplications.Add(studentApplication);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Application Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("GetApplications")]
        public IActionResult GetApplications()
        {
            try
            {
                var studentApplications = _context.ScholarshipApplications.ToList();

                if (!studentApplications.Any()) return BadRequest("No Application is present");

                return Ok(studentApplications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetApplication/{id}")]
        public IActionResult GetApplication(int id)
        {
            try
            {
                var studentApplication = _context.ScholarshipApplications.Find(id);

                if (studentApplication == null) return NotFound();
                
                return Ok(studentApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CheckApplicationStatus/{id}")]
        public IActionResult GetStatus(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);
                if (application.ApprovedByOfficer && application.ApprovedByMinistry && application.ApprovedByInstitute)
                    return Ok("Scholarship Granted");
                else
                    return Ok("Status Pending");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
