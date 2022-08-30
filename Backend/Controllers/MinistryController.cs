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

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] MinistryLoginDto ministryLoginDto)
        {
            try
            {
                var ministryUser = _context.Ministries
                    .Where(ministry => ministry.MinistryEmail == ministryLoginDto.Email)
                    .FirstOrDefault();

                if (ministryUser == null)
                {
                    return NotFound("User not found");
                }

                if (!VerifyPasswordHash(ministryLoginDto.Password, ministryUser.PasswordHash, ministryUser.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                string token = GenerateToken(ministryUser);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

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

        [HttpGet("PendingInstitutes/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult GetInstitutes(int id)
        {
            try
            {
                var pendingInstitute = _context.Institutes.Find(id);

                if (pendingInstitute == null) return NotFound();

                return Ok(pendingInstitute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

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

        [HttpGet("PendingApplications/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult GetApplications(int id)
        {
            try
            {
                var pendingApplication = _context.ScholarshipApplications.Find(id);

                if (pendingApplication == null) return NotFound();

                return Ok(pendingApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost("VerifyInstitute/{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult VerifyInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound("Institute not found");

                if (!institute.ApprovedByOfficer) return BadRequest("Officer needs to approve before ministry");

                if (institute.ApprovedByMinistry) return BadRequest("Already approved by ministry");

                institute.ApprovedByMinistry = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost("VerifyApplication/{id}")]
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

                application.ApprovedByMinistry = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("RejectApplication/{id}")]
        //reject the scholarship application
        public IActionResult RejectApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications
                    .Where(application => application.ApplicationId == id)
                    .FirstOrDefault();

                application.IsRejected = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("RejectInstitute/{id}")]
        //reject the scholarship application
        public IActionResult RejectInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes
                    .Where(institute => institute.InstituteId == id)
                    .FirstOrDefault();

                institute.IsRejected = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private string GetMinistryEmail()
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