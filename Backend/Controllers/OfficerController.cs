using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System;
using System.Linq;
using Backend.DTOs.Officer;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficerController : Controller
    {
        private readonly ScholarshipDbContext _context;
        private readonly IConfiguration _configuration;

        public OfficerController(ScholarshipDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// This method is used to sign in the nodal officer. The credentials are checked and if they match 
        /// with database they are logged in.
        /// </summary>
        /// <param name="officerLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] OfficerLoginDto officerLoginDto)
        {
            try
            {
                var officerUser = _context.NodalOfficers
                    .Where(officer => officer.OfficerEmail == officerLoginDto.Email)
                    .FirstOrDefault();

                if (officerUser == null) return NotFound("User not found");
                
                if (!VerifyPasswordHash(officerLoginDto.Password, officerUser.PasswordHash, officerUser.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                string token = GenerateToken(officerUser);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get a list of institutes pending verification by officer
        /// </summary>
        /// <returns></returns>
        [HttpGet("PendingInstitutes")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetInstitutes()
        {
            try
            {
                var pendingInstitutes = _context.Institutes
                    .Where(x => !x.ApprovedByOfficer && !x.IsRejected)
                    .ToList();

                if (!pendingInstitutes.Any()) return NotFound("No application pending");

                return Ok(pendingInstitutes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get details of pending institute by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("PendingInstitutes/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetInstitute(int id)
        {
            try
            {
                var pendingInstitute = _context.Institutes.Find(id);

                if (pendingInstitute == null) return NotFound("Institute not found");

                if (pendingInstitute.ApprovedByOfficer) return BadRequest("Institute already verified by officer");

                return Ok(pendingInstitute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get all the applications which are not approved by officer
        /// </summary>
        /// <returns></returns>
        [HttpGet("PendingApplications")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetApplications()
        {
            try
            {
                var pendingApplications = _context.ScholarshipApplications
                    .Where(x => !x.ApprovedByOfficer && x.ApprovedByInstitute && !x.IsRejected)
                    .ToList();

                if (!pendingApplications.Any()) return BadRequest("Application not found");

                return Ok(pendingApplications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get details of application by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("PendingApplications/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound("Application not found");

                if (!application.ApprovedByInstitute) return BadRequest("Institute needs to approve application before officer");

                if (application.ApprovedByOfficer) return BadRequest("Already approved by officer");

                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To verify a institute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("VerifyInstitute/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult VerifyInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound();

                if (institute.ApprovedByOfficer) return BadRequest("Already approved by officer");

                if (institute.IsRejected) return BadRequest("You cannot verify a rejected institute");

                institute.ApprovedByOfficer = true;

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something went wrong....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To verify a application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("VerifyApplication/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult VerifyApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound();

                if (!application.ApprovedByInstitute) return BadRequest("Institute needs to approve before officer");

                if (application.ApprovedByOfficer) return BadRequest("Already approved by officer");

                if (application.IsRejected) return BadRequest("You cannot approve a rejected application");

                application.ApprovedByOfficer = true;

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something went wrong....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To reject a application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RejectApplication/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult RejectApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound("No such application found");

                if (application.IsRejected) return BadRequest("Application is already rejected");

                if (!application.ApprovedByInstitute) return BadRequest("Institute needs to approve before officer and ministry");

                if (application.ApprovedByOfficer) return BadRequest("You cannot reject an approved application");

                application.IsRejected = true;

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something went wrong....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To reject a institute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RejectInstitute/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult RejectInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound("Institute not found");

                if (institute.IsRejected) return BadRequest("Institute already rejected");

                if (institute.ApprovedByOfficer) return BadRequest("You cannot reject a verified institute");

                institute.IsRejected = true;

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something went wrong....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To get the email of the logged in nodal officer
        /// </summary>
        /// <returns></returns>
        private string GetOfficerEmail()
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
        /// <param name="officer"></param>
        /// <returns></returns>
        private string GenerateToken(NodalOfficer officer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, officer.OfficerEmail),
                new Claim(ClaimTypes.Role, "Officer")
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
