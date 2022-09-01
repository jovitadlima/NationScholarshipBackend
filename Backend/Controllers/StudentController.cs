using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs.Student;
using Backend.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;

using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ScholarshipDbContext _context;
        private readonly IConfiguration _configuration;

        public StudentController(ScholarshipDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// This method is used to register a student. It checks if the all the credentials match. Then it saves the 
        /// student data in the database.
        /// </summary>
        /// <param name="studentRegisterDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] StudentRegisterDto studentRegisterDto)
        {
            try
            {
                var checkStudent = _context.Students.Any(x => x.AadharNumber == studentRegisterDto.AadharNumber);

                if (checkStudent) return BadRequest("Student already exists");

                var checkInstitute = _context.Institutes
                    .Any(x => x.InstituteCode == studentRegisterDto.InstituteCode
                        && x.ApprovedByMinistry && x.ApprovedByOfficer && !x.IsRejected);

                if (!checkInstitute) return BadRequest($"Institute with code {studentRegisterDto.InstituteCode} does not exists");

                if (studentRegisterDto.Password != studentRegisterDto.Password2)
                {
                    return BadRequest("Password doest match with confirm password");
                }

                CreatePasswordHash(studentRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var student = new Student()
                {
                    StudentName = studentRegisterDto.StudentName,
                    DateOfBirth = studentRegisterDto.DateOfBirth,
                    Gender = studentRegisterDto.Gender,
                    Email = studentRegisterDto.Email,
                    AadharNumber = studentRegisterDto.AadharNumber,
                    InstituteCode = studentRegisterDto.InstituteCode,
                    PhoneNo = studentRegisterDto.PhoneNo,
                    StateOfDomicile = studentRegisterDto.StateOfDomicile,
                    District = studentRegisterDto.District,
                    BankIfscCode = studentRegisterDto.BankIfscCode,
                    BankAccountNumber = studentRegisterDto.BankAccountNumber,
                    BankName = studentRegisterDto.BankName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var institute = _context.Institutes
                    .Where(x => x.InstituteCode == student.InstituteCode)
                    .FirstOrDefault();

                student.InstituteId = institute.InstituteId;

                _context.Students.Add(student);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Registration failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// This method is used to sign in a student. The credentials are checked and if they match 
        /// with database they are logged in.
        /// </summary>
        /// <param name="studentLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult Login([FromBody] StudentLoginDto studentLoginDto)
        {
            try
            {
                var studentUser = _context.Students
                    .Where(student => student.AadharNumber == studentLoginDto.AadharNumber)
                    .FirstOrDefault();

                if (studentUser == null)
                {
                    return NotFound("User not found");
                }

                if (!VerifyPasswordHash(studentLoginDto.Password, studentUser.PasswordHash, studentUser.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                string token = GenerateToken(studentUser);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get all the registered students 
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllStudents")]
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

        /// <summary>
        /// To get details of the logged in student
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult GetStudent()
        {
            try
            {
                var student = _context.Students
                    .Where(x => x.AadharNumber == GetStudentAadharNumber())
                    .FirstOrDefault();

                if (student == null) return NotFound("No student present");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To delete a student. Only ministry (admin user) is authorized to delete a 
        /// student from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var student = _context.Students.Find(id);

                _context.Students.Remove(student);

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To delete an application. Only ministry is authorized
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteApplication/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult DeleteApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                _context.ScholarshipApplications.Remove(application);

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To create a scholarship application form for the logged in student. Only student is
        /// authorized to create an application
        /// </summary>
        /// <param name="studentApplicationDto"></param>
        /// <returns></returns>
        [HttpPost("CreateApplication")]
        [Authorize(Roles = "Student")]
        public IActionResult CreateApplication([FromBody] StudentApplicationDto studentApplicationDto)
        {
            try
            {
                var student = _context.Students
                    .Where(x => x.AadharNumber == GetStudentAadharNumber())
                    .FirstOrDefault();

                if (studentApplicationDto.AadharNumber != GetStudentAadharNumber())
                {
                    return BadRequest("Aadhar Number is not eqaul");
                }

                if (studentApplicationDto.InstituteCode != student.InstituteCode)
                {
                    return BadRequest("Institute Code is not equal");
                }

                var appliedApplications = _context.ScholarshipApplications.Where(a=> a.AadharNumber == GetStudentAadharNumber());

                int checkSchemeId = Convert.ToInt32(studentApplicationDto.SchemeId);

                var check = appliedApplications.Any(app => app.SchemeId == checkSchemeId);

                if (check) return BadRequest("You have already applied for this scheme");

                var studentApplication = new ScholarshipApplication()
                {
                    AadharNumber = studentApplicationDto.AadharNumber,
                    Community = studentApplicationDto.Community,
                    Religion = studentApplicationDto.Religion,
                    FatherName = studentApplicationDto.FatherName,
                    MotherName = studentApplicationDto.MotherName,
                    AnnualIncome = studentApplicationDto.AnnualIncome,
                    InstituteName = studentApplicationDto.InstituteName,
                    PresentCourse = studentApplicationDto.PresentCourse,
                    PresentCourseYear = studentApplicationDto.PresentCourseYear,
                    ModeOfStudy = studentApplicationDto.ModeOfStudy,
                    ClassStartDate = studentApplicationDto.ClassStartDate,
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
                    StudentId = student.StudentId,
                    SchemeId = Convert.ToInt32(studentApplicationDto.SchemeId),
                    InstituteCode = studentApplicationDto.InstituteCode,
                    DomicileCertificate = studentApplicationDto.DomicileCertificate,
                    Photo = studentApplicationDto.Photo,
                    InstituteIdCard = studentApplicationDto.InstituteIdCard,
                    CasteOrIncomeCertificate = studentApplicationDto.CasteOrIncomeCertificate,
                    PreviousYearMarksheet = studentApplicationDto.PreviousYearMarksheet,
                    FeeReceiptOfCurrentYear = studentApplicationDto.FeeReceiptOfCurrentYear,
                    BankPassBook = studentApplicationDto.BankPassBook,
                    AadharCard = studentApplicationDto.AadharCard,
                    MarkSheet10 = studentApplicationDto.MarkSheet10,
                    MarkSheet12 = studentApplicationDto.MarkSheet12
                };

                _context.ScholarshipApplications.Add(studentApplication);

                var result = _context.SaveChanges() > 0;

                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get all the applications from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllApplications")]
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

        /// <summary>
        /// To get all the applications which are pending approval form authorities
        /// </summary>
        /// <returns></returns>
        [HttpGet("PendingApplications")]
        [Authorize(Roles = "Student")]
        public IActionResult GetApplication()
        {
            try
            {
                var pendingStudentApplication = _context.ScholarshipApplications
                    .Where(x => x.AadharNumber == GetStudentAadharNumber())
                    .ToList();

                if (!pendingStudentApplication.Any()) return NotFound("No Application Pending");

                return Ok(pendingStudentApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To check the status of the application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("CheckApplicationStatus/{id}")]
        [Authorize(Roles = "Student")]
        public IActionResult GetStatus(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound("Application not found");

                var instituteApprovalStatus = application.ApprovedByInstitute;
                var officerApprovalStatus = application.ApprovedByOfficer;
                var ministryApprovalStatus = application.ApprovedByMinistry;

                bool status = instituteApprovalStatus && officerApprovalStatus && ministryApprovalStatus;

                return Ok(new { Application = application, Status = status });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get the aadhar number of the logged in student
        /// </summary>
        /// <returns></returns>
        private string GetStudentAadharNumber()
        {
            return User?.Identity?.Name;
        }

        /// <summary>
        /// To generate a hash of the password to securely store the password in database
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// To verify if the password provided by user matched with the hashed password
        /// stored in the database
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        /// <summary>
        /// This method is used to generate a jwt token.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private string GenerateToken(Student student)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, student.AadharNumber),
                new Claim(ClaimTypes.Role, "Student")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
