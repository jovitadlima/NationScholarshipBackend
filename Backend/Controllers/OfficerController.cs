using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetInstitutes/{id}")]  //To get institutes by id
        public IActionResult GetInstitutes(int id)
        {
            var institute = _context.Institutes.Find(id);

            if (institute == null) return NotFound();


            return Ok(institute);
        }

        [HttpGet("GetApplications")]
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

        [HttpGet("GetApplications/{id}")]  //To get student applications by id
        public IActionResult GetApplications(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                if (application == null)
                {
                    return NotFound();
                }

                return Ok(application);
            }
            catch(Exception ex) { 
                return BadRequest(ex.InnerException.Message);
            } 
        }

        [HttpPost]
        [Route("VerifyInstitute/{id}")]
        public IActionResult VerifyInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes
                    .Where(institute => institute.InstituteId == id)
                    .FirstOrDefault();

                institute.ApprovedByOfficer = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("VerifyApplication/{id}")]
        public IActionResult VerifyApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications
                    .Where(application => application.ApplicationId == id)
                    .FirstOrDefault();

                application.ApprovedByOfficer = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
