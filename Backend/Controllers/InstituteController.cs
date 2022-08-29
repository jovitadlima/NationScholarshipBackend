using Backend.DTOs.Institute;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : ControllerBase
    {
        private readonly ScholarshipDbContext _context;
        private readonly IConfiguration _configuration;
        public InstituteController(ScholarshipDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromForm] IFormFile file1, [FromForm] IFormFile file2,
            [FromForm] InstituteRegisterDto instituteRegisterDto)
        {
            try
            {
                if (instituteRegisterDto.Password != instituteRegisterDto.Password2)
                {
                    return BadRequest("Password doest match with confirm password");
                }

                CreatePasswordHash(instituteRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var institute = new Institute()
                {
                    InstituteCategory = instituteRegisterDto.InstituteCategory,
                    State = instituteRegisterDto.State,
                    District = instituteRegisterDto.District,
                    InstituteName = instituteRegisterDto.InstituteName,
                    InstituteCode = instituteRegisterDto.InstituteCode,
                    DiseCode = instituteRegisterDto.DiseCode,
                    Location = instituteRegisterDto.Location,
                    InstituteType = instituteRegisterDto.InstituteType,
                    AffliatedUniversityState = instituteRegisterDto.AffliatedUniversityState,
                    AffliatedUniversityName = instituteRegisterDto.AffliatedUniversityName,
                    YearOfAddmission = instituteRegisterDto.YearOfAddmission,
                    AddressLine1 = instituteRegisterDto.AddressLine1,
                    AddressLine2 = instituteRegisterDto.AddressLine2,
                    AddressCity = instituteRegisterDto.AddressCity,
                    AddressState = instituteRegisterDto.AddressState,
                    AddressDistrict = instituteRegisterDto.AddressDistrict,
                    AddressPincode = instituteRegisterDto.AddressPincode,
                    PrincipalName = instituteRegisterDto.PrincipalName,
                    MobileNumber = instituteRegisterDto.MobileNumber,
                    Telephone = instituteRegisterDto.Telephone,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                string directoryPath = @"D:\Document_Project\InstituteDocument\" + institute.InstituteCode;
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filepath1 = Path.Combine(directoryPath, file1.FileName);
                string filepath2 = Path.Combine(directoryPath, file2.FileName);
                using (var stream = new FileStream(filepath1, FileMode.Create))
                {
                    file1.CopyTo(stream);
                }
                using (var stream = new FileStream(filepath2, FileMode.Create))
                {
                    file2.CopyTo(stream);
                }
                institute.RegistrationCertificate = filepath1;
                institute.UniversityAffliationCertificate = filepath2;

                _context.Institutes.Add(institute);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(institute);
                }
                else
                {
                    return BadRequest("Failed to create Institute");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] InstituteLoginDto studentLoginDto)
        {
            try
            {
                var instituteUser = _context.Institutes
                    .Where(institute => institute.InstituteCode == studentLoginDto.InstituteCode)
                    .FirstOrDefault();

                if (instituteUser == null)
                {
                    return NotFound("User not found");
                }

                if (!VerifyPasswordHash(studentLoginDto.Password, instituteUser.PasswordHash, instituteUser.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                string token = GenerateToken(instituteUser);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private string GenerateToken(Institute institute)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, institute.InstituteCode),
                new Claim(ClaimTypes.Role, "Institute")
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

        // REMOVE TAKING ID
        [HttpGet]
        [Authorize(Roles = "Institute")]
        public IActionResult GetInstitute()
        {
            try
            {
                var institute = _context.Institutes
                    .Where(x => x.InstituteCode == GetInstituteCode())
                    .FirstOrDefault();

                if (institute == null) return NotFound();

                return Ok(institute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // REMOVE TAKING ID
        [HttpGet("AllStudents")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var institute = _context.Institutes
                    .Where(x => x.InstituteCode == GetInstituteCode())
                    .FirstOrDefault();

                int instId = institute.InstituteId;

                var students = _context.Students
                    .Where(x => x.InstituteId == instId)
                    .ToList();

                if (!students.Any()) return NotFound("No Students");

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // REMOVE TAKING ID
        [HttpGet("PendingApplications")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetApplications()
        {
            try
            {
                var applications = _context.ScholarshipApplications
                    .Where(app => app.InstituteCode == GetInstituteCode() && !app.ApprovedByInstitute)
                    .ToList();

                if (!applications.Any()) return NotFound("No Applications");

                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("PendingApplications/{id}")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application.InstituteCode != GetInstituteCode())
                {
                    return BadRequest("You cannot view application of students from different institute");
                }

                if (application == null) return NotFound("Application not found");

                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // REMOVE TAKING ID
        [HttpGet("CheckRegistrationStatus")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetStatus()
        {
            try
            {
                var institute = _context.Institutes
                    .Where(x => x.InstituteCode == GetInstituteCode())
                    .FirstOrDefault();

                var officerApprovalStatus = institute.ApprovedByOfficer;
                var ministryApprovalStatus = institute.ApprovedByMinistry;

                if (officerApprovalStatus && ministryApprovalStatus)
                {
                    return Ok("Status Approved");
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

        [HttpPost("VerifyPendingApplication/{id}")]
        [Authorize(Roles = "Institute")]
        public IActionResult PostVerifyApplication(int id, IFormFile file)
        {
            try
            {
                var application = _context.ScholarshipApplications
                    .Where(app => app.ApplicationId == id)
                    .FirstOrDefault();

                if (application == null) return NotFound("No such application");

                if (application.InstituteCode != GetInstituteCode())
                {
                    return BadRequest("You cannot approve applications of student from different institute");
                }

                application.ApprovedByInstitute = true;

                if (application.ApprovedByInstitute)
                {
                    string directorypath = @"D:/Certificates-Bonafide/" + application.StudentId;
                    if (!Directory.Exists(directorypath))
                    {
                        Directory.CreateDirectory(directorypath);
                    }
                    string filepath = Path.Combine(directorypath, file.FileName);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    application.CertificateUrl = filepath;

                    _context.SaveChanges();
                    return Ok("Approved");
                }
                else
                {
                    return BadRequest("Application Rejected");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // NOT REQUIRED, REMOVE LATER
        [HttpDelete("{id}")]
        public IActionResult DeleteInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                _context.Institutes.Remove(institute);
                var result = _context.SaveChanges() > 0;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetInstituteCode()
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
