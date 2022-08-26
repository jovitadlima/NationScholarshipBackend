using Microsoft.AspNetCore.Mvc;
using Backend.DTOs.Student;
using Backend.Models;
using System;
using System.Linq;
namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficerController : Controller
    {
        private ScholarshipDbContext _context;
        public OfficerController(ScholarshipDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetInstitutes")]
        public IActionResult GetInstitutes()
        {
            try
            {
                var institute = _context.Institutes.ToList();

                return Ok(institute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]  //To get institutes by id
        public IActionResult GetInstitutes(int id)
        {
            var institute = _context.Institutes.Find(id);
            if (institute == null)
            {
                return NotFound();
            }
            return Ok(institute);
        }

        [HttpGet("GetApplications")]
        public IActionResult GetApplications()
        {
            try
            {
                var application = _context.ScholarshipApplications.ToList();

                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]  //To get student applications by id
        public IActionResult GetApplications(int id)
        {
            var application = _context.ScholarshipApplications.Find(id);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        [HttpPost]
        [Route("VerifyInstitute/{id}")]
        public IActionResult VerifyInstitute(int id, [FromBody] Institute Institute)
        {
            try
            {
                var institute = _context.Institutes.Where(institute => institute.InstituteId == id).FirstOrDefault();
                institute.ApprovedByOfficer = true;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("VerifyApplication/{id}")]
        public IActionResult VerifyApplication(int id, [FromBody] ScholarshipApplication ScholarshipApplication)
        {
            try
            {
                var application = _context.ScholarshipApplications.Where(application => ScholarshipApplication.ApplicationId == id).FirstOrDefault();
                application.ApprovedByOfficer = true;
                _context.SaveChanges();
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
