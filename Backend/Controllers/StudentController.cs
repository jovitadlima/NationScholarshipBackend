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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] StudentRegisterDto studentRegisterDto)
        {
            try
            {
                if (studentRegisterDto.Password != studentRegisterDto.Password2)
                {
                    return BadRequest("Password doest match with confirm password");
                }

                CreatePasswordHash(studentRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);


                var student = new Student()
                {
                    StudentName = studentRegisterDto.StudentName,
                    DateOfBirth = Convert.ToDateTime(studentRegisterDto.DateOfBirth),
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

                // create institute first
                var institute = _context.Institutes
                    .Where(institute => institute.InstituteCode == student.InstituteCode)
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
                    return BadRequest("Failed to create student");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] StudentLoginDto studentLoginDto)
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
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

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

        // Not Required
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

        // Change later remove id
        [HttpGet]
        [Authorize(Roles = "Student")]
        public IActionResult GetStudent()
        {
            try
            {
                var student = _context.Students
                    .Where(x => x.AadharNumber == GetStudentAadharNumber())
                    .FirstOrDefault();

                if (student == null) return NotFound();

                return Ok(student);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // Remove later, only admin should be able to do this
        [HttpDelete("{id}")]
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

        // revove this later, only admin should be able to do this
        [HttpDelete("DeleteApplication/{id}")]
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

        // needs to be modified, student id should be taken from jwt token not passed by json request
        [HttpPost("CreateApplication")]
        [Authorize(Roles = "Student")]
        public IActionResult CreateApplication([FromForm] IFormFile file1, [FromForm] IFormFile file2, [FromForm] IFormFile file3, [FromForm] IFormFile file4, [FromForm] IFormFile file5, [FromForm] IFormFile file6, [FromForm] IFormFile file7, [FromForm] IFormFile file8, [FromForm] IFormFile file9, [FromForm] IFormFile file10,
            [FromForm] StudentApplicationDto studentApplicationDto)
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
                    StudentId = student.StudentId,
                    SchemeId = studentApplicationDto.SchemeId,
                    InstituteCode = studentApplicationDto.InstituteCode
                };

                string directoryPath = @"D:\Document_Project\ScholarshipApplicationDocuments\" + GetStudentAadharNumber();
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filepath1 = Path.Combine(directoryPath, file1.FileName);
                string filepath2 = Path.Combine(directoryPath, file2.FileName);
                string filepath3 = Path.Combine(directoryPath, file3.FileName);
                string filepath4 = Path.Combine(directoryPath, file4.FileName);
                string filepath5 = Path.Combine(directoryPath, file5.FileName);
                string filepath6 = Path.Combine(directoryPath, file6.FileName);
                string filepath7 = Path.Combine(directoryPath, file7.FileName);
                string filepath8 = Path.Combine(directoryPath, file8.FileName);
                string filepath9 = Path.Combine(directoryPath, file9.FileName);
                string filepath10 = Path.Combine(directoryPath, file10.FileName);
                using (var stream = new FileStream(filepath1, FileMode.Create))
                {
                    file1.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath2, FileMode.Create))
                {
                    file2.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath3, FileMode.Create))
                {
                    file3.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath4, FileMode.Create))
                {
                    file4.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath5, FileMode.Create))
                {
                    file5.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath6, FileMode.Create))
                {
                    file6.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath7, FileMode.Create))
                {
                    file7.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath8, FileMode.Create))
                {
                    file8.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath9, FileMode.Create))
                {
                    file9.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath10, FileMode.Create))
                {
                    file10.CopyTo(stream);
                }

                studentApplication.DomicileCertificate = filepath1;
                studentApplication.Photo = filepath2;
                studentApplication.InstituteIdCard = filepath3;
                studentApplication.CasteOrIncomeCertificate = filepath4;
                studentApplication.PreviousYearMarksheet = filepath5;
                studentApplication.FeeReceiptOfCurrentYear = filepath6;
                studentApplication.BankPassBook = filepath7;
                studentApplication.AadharCard = filepath8;
                studentApplication._10thMarkSheet = filepath9;
                studentApplication._12thMarkSheet = filepath10;


                _context.ScholarshipApplications.Add(studentApplication);

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // remove this later
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

        // modify to remove taking id
        [HttpGet("PendingApplications")]
        [Authorize(Roles = "Student")]
        public IActionResult GetApplication()
        {
            try
            {
                var pendingStudentApplication = _context.ScholarshipApplications
                    .Where(x => x.AadharNumber == GetStudentAadharNumber())
                    .ToList();

                if (!pendingStudentApplication.Any()) return NotFound();
                
                return Ok(pendingStudentApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("CheckApplicationStatus/{id}")]
        [Authorize(Roles = "Student")]
        public IActionResult GetStatus(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                var instituteApprovalStatus = application.ApprovedByInstitute;
                var officerApprovalStatus = application.ApprovedByOfficer;
                var ministryApprovalStatus = application.ApprovedByMinistry;

                if (instituteApprovalStatus && officerApprovalStatus && ministryApprovalStatus)
                {
                    return Ok("Scholarship Granted");
                }
                else
                {
                    return BadRequest("Status Pending");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private string GetStudentAadharNumber()
        {
            return User?.Identity?.Name;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
