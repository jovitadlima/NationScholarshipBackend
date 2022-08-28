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

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] OfficerLoginDto officerLoginDto)
        {
            try
            {
                var officerUser = _context.NodalOfficers
                    .Where(officer => officer.OfficerEmail == officerLoginDto.Email)
                    .FirstOrDefault();

                if (officerUser == null)
                {
                    return NotFound("User not found");
                }

                if (!VerifyPasswordHash(officerLoginDto.Password, officerUser.PasswordHash, officerUser.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }

                string token = GenerateToken(officerUser);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        private string GenerateToken(NodalOfficer officer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, officer.OfficerEmail),
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

        [HttpGet("GetPendingInstitutes")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetInstitutes()
        {
            try
            {
                var pendingInstitutes = _context.Institutes
                    .Where(x => !x.ApprovedByOfficer)
                    .ToList();

                if (!pendingInstitutes.Any()) return NotFound("No application pending");

                return Ok(pendingInstitutes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("GetPendingInstitutes/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetInstitutes(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound();

                return Ok(institute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("GetPendingApplications")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetApplications()
        {
            try
            {
                var application = _context.ScholarshipApplications
                    .Where(x => !x.ApprovedByOfficer)
                    .ToList();

                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("GetPendingApplications/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult GetApplications(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound();

                return Ok(application);
            }
            catch(Exception ex) { 
                return BadRequest(ex.InnerException.Message);
            } 
        }

        [HttpPost("VerifyInstitute/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult VerifyInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);

                if (institute == null) return NotFound();

                institute.ApprovedByOfficer = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("VerifyApplication/{id}")]
        [Authorize(Roles = "Officer")]
        public IActionResult VerifyApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null) return NotFound();

                application.ApprovedByOfficer = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
