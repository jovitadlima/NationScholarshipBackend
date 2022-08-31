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

        /// <summary>
        /// This method is used to register an institute. It checks if the all the credentials match. Then it saves the 
        /// institute data in the database.
        /// </summary>
        /// <param name="instituteRegisterDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] InstituteRegisterDto instituteRegisterDto)
        {
            try
            {
                if (instituteRegisterDto.Password != instituteRegisterDto.Password2)
                {
                    return BadRequest("Password does not match with confirm password");
                }

                var check = _context.Institutes.Any(x => x.InstituteCode == instituteRegisterDto.InstituteCode);

                if (check) return BadRequest("Institute already exists");

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

                _context.Institutes.Add(institute);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(new { Result = result });
                }
                else
                {
                    return BadRequest("Registration Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// This method is used to sign in a institute. The credentials are checked and if they match 
        /// with database they are logged in. If the institute is not verified then they will not be able 
        /// to login to the portal.
        /// </summary>
        /// <param name="studentLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] InstituteLoginDto studentLoginDto)
        {
            try
            {
                var instituteUser = _context.Institutes
                    .Where(institute => institute.InstituteCode == studentLoginDto.InstituteCode)
                    .FirstOrDefault();

                if (instituteUser == null) return NotFound("User not found");


                if (!GetRegistrationStatus(instituteUser))
                {
                    return BadRequest("You are not verified by officer or ministry");
                }

                if (!VerifyPasswordHash(studentLoginDto.Password, instituteUser.PasswordHash, instituteUser.PasswordSalt))
                {
                    return NotFound("Wrong password");
                }

                string token = GenerateToken(instituteUser);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// This method is used to generate a jwt token.
        /// </summary>
        /// <param name="institute"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To get details of the logged in institute.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Institute")]
        public IActionResult GetInstitute()
        {
            try
            {
                var institute = _context.Institutes
                    .Where(x => x.InstituteCode == GetInstituteCode())
                    .FirstOrDefault();

                if (institute == null) return NotFound("Intitute does not exist");

                return Ok(institute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get details of all the students studying in the institute
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllStudents")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _context.Students
                    .Where(x => x.InstituteCode == GetInstituteCode())
                    .ToList();

                if (!students.Any()) return NotFound("No Students present");

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Get details of all the registered institutes
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllInstitutes")]
        public IActionResult GetAllInstitutes()
        {
            try
            {
                var institutes = _context.Institutes.ToList();

                if (!institutes.Any()) return NotFound("No Student Present");

                return Ok(institutes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get all the applications of students which are pending approval from institute.
        /// Institute can get application of students studying in that institute
        /// </summary>
        /// <returns></returns>
        [HttpGet("PendingApplications")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetApplications()
        {
            try
            {
                var applications = _context.ScholarshipApplications
                    .Where(app => app.InstituteCode == GetInstituteCode() && !app.ApprovedByInstitute && !app.IsRejected)
                    .ToList();

                if (!applications.Any()) return NotFound("No application pending approval");

                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get all the application of student by id which is pending approval from institute.
        /// Institute can get application of students studying in that institute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// To check if the institute has been verified by the authorities
        /// </summary>
        /// <param name="institute"></param>
        /// <returns></returns>
        private bool GetRegistrationStatus(Institute institute)
        {
            var officerApprovalStatus = institute.ApprovedByOfficer;
            var ministryApprovalStatus = institute.ApprovedByMinistry;

            bool status = officerApprovalStatus && ministryApprovalStatus;
            return status;

        }

        /// <summary>
        /// To get all the applications which are approved by the logged in institute
        /// </summary>
        /// <returns></returns>
        [HttpGet("ApprovedApplications")]
        [Authorize(Roles = "Institute")]
        public IActionResult GetApprovedApplications()
        {
            try
            {
                var applications = _context.ScholarshipApplications
                    .Where(app => app.InstituteCode == GetInstituteCode() && app.ApprovedByInstitute && !app.IsRejected)
                    .ToList();

                if (!applications.Any()) return NotFound("No Applications found");

                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To approve the application of student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("VerifyPendingApplication/{id}")]
        [Authorize(Roles = "Institute")]
        public IActionResult VerifyApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications
                    .Where(app => app.ApplicationId == id)
                    .FirstOrDefault();

                if (application == null) return NotFound("No such application");

                if (application.ApprovedByInstitute) return BadRequest("Application is already approved");

                if (application.IsRejected) return BadRequest("You cannot approve a rejected application");

                if (application.InstituteCode != GetInstituteCode())
                {
                    return BadRequest("You cannot approve applications of student from different institute");
                }

                application.ApprovedByInstitute = true;

                bool result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something went wrong...");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To reject the application of the student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RejectPendingApplication/{id}")]
        [Authorize(Roles = "Institute")]
        public IActionResult RejectApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications
                    .Where(app => app.ApplicationId == id)
                    .FirstOrDefault();

                if (application == null) return NotFound("No such application");

                if (application.IsRejected) return BadRequest("Application already rejected");

                if (application.ApprovedByInstitute) return BadRequest("You cannot reject an approved application");

                if (application.InstituteCode != GetInstituteCode())
                {
                    return BadRequest("You cannot approve applications of student from different institute");
                }

                application.IsRejected = true;

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok("Application Rejected");
                }
                else
                {
                    return BadRequest("Something went wrong...");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To delete an institute. Only ministry is authorize to delete an institute from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Ministry")]
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

        /// <summary>
        /// To get the institute code of the logged in institute
        /// </summary>
        /// <returns></returns>
        private string GetInstituteCode()
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
    }
}