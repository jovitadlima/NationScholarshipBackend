using Backend.DTOs.Ministry;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinistryController : Controller
    {
        private readonly ScholarshipDbContext _context;
        private readonly IConfiguration _configuration;

        public MinistryController(ScholarshipDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// This method is used to sign in the ministry. The credentials are checked and if they match 
        /// with database they are logged in.
        /// </summary>
        /// <param name="ministryLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] MinistryLoginDto ministryLoginDto)
        {
            try
            {
                var ministryUser = _context.Ministries
                    .Where(ministry => ministry.MinistryEmail == ministryLoginDto.Email)
                    .FirstOrDefault();

                if (ministryUser == null) return NotFound("User not found");

                if (!VerifyPasswordHash(ministryLoginDto.Password, ministryUser.PasswordHash, ministryUser.PasswordSalt))
                {
                    return BadRequest("Wrong password");
                }

                string token = GenerateToken(ministryUser);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get a list of institutes pending verification by ministry
        /// </summary>
        /// <returns></returns>
        [HttpGet("PendingInstitutes")]
        [Authorize(Roles = "Ministry")]
        public IActionResult GetInstitutes()
        {
            try
            {
                var pendingInstitutes = _context.Institutes
                    .Where(x => x.ApprovedByOfficer && !x.ApprovedByMinistry && !x.IsRejected)
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
        [Authorize(Roles = "Ministry")]
        public IActionResult GetInstitutes(int id)
        {
            try
            {
                var pendingInstitute = _context.Institutes.Find(id);

                if (pendingInstitute == null) return NotFound("No institute found");

                if (!pendingInstitute.ApprovedByOfficer) return BadRequest("Officer needs to verify institute before minstry");

                if (pendingInstitute.ApprovedByMinistry) return BadRequest("Institute already verified by ministry");

                return Ok(pendingInstitute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get all the applications which are not approved by ministry
        /// </summary>
        /// <returns></returns>
        [HttpGet("PendingApplications")]
        [Authorize(Roles = "Ministry")]
        public IActionResult GetApplications()
        {
            try
            {
                var pendingApplications = _context.ScholarshipApplications
                    .Where(app => app.ApprovedByInstitute && app.ApprovedByOfficer && !app.ApprovedByMinistry && !app.IsRejected)
                    .ToList();

                if (!pendingApplications.Any()) return BadRequest("No application present");

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
        [Authorize(Roles = "Ministry")]
        public IActionResult GetApplications(int id)
        {
            try
            {
                var pendingApplication = _context.ScholarshipApplications.Find(id);

                if (pendingApplication == null) return NotFound();

                if (!pendingApplication.ApprovedByInstitute) return BadRequest("Institute needs to approve application before officer");

                if (!pendingApplication.ApprovedByOfficer) return BadRequest("Officer needs to approve application before minstry");

                if (pendingApplication.ApprovedByMinistry) return BadRequest("Already approved by ministry");

                return Ok(pendingApplication);
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
        [Authorize(Roles = "Ministry")]
        public IActionResult VerifyInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound("Institute not found");

                if (!institute.ApprovedByOfficer) return BadRequest("Officer needs to approve before ministry");

                if (institute.ApprovedByMinistry) return BadRequest("Already approved by ministry");

                if (institute.IsRejected) return BadRequest("You cannot verify a rejected institute");

                institute.ApprovedByMinistry = true;

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
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To verify a application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("VerifyApplication/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult VerifyApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound("Application not found");

                if (!application.ApprovedByInstitute) return BadRequest("Institute needs to approve before officer and ministry");

                if (!application.ApprovedByOfficer) return BadRequest("Officer needs to approve before ministry");

                if (application.ApprovedByMinistry) return BadRequest("Already approved by ministry");

                if (application.IsRejected) return BadRequest("You cannot approve a rejected application");

                application.ApprovedByMinistry = true;

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
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To reject a application
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RejectApplication/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult RejectApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound("No such application found");

                if (application.IsRejected) return BadRequest("Application is already rejected");

                if (!application.ApprovedByInstitute) return BadRequest("Institute needs to approve before officer and ministry");

                if (!application.ApprovedByOfficer) return BadRequest("Officer needs to approve before ministry");

                if (application.ApprovedByMinistry) return BadRequest("You cannot reject an approved application");

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
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To reject a institute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RejectInstitute/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult RejectInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound("Institute not found");

                if (institute.IsRejected) return BadRequest("Institute already rejected");

                if (!institute.ApprovedByOfficer) return BadRequest("Officer needs to verify the institute before ministry");

                if (institute.ApprovedByMinistry) return BadRequest("You cannot reject a verified institute");

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
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get the email of the logged in ministry
        /// </summary>
        /// <returns></returns>
        private string GetMinistryEmail()
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
        /// <param name="ministry"></param>
        /// <returns></returns>
        private string GenerateToken(Ministry ministry)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ministry.MinistryEmail),
                new Claim(ClaimTypes.Role, "Ministry")
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