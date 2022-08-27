using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinistryController : Controller
    {
        private ScholarshipDbContext _context;

        public MinistryController(ScholarshipDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetInstitutes")]
        public IActionResult GetInstitutes()
        {
            try
            {
                var pendingInstitutes = _context.Institutes
                    .Where(x => x.ApprovedByOfficer && !x.ApprovedByMinistery)
                    .ToList();

                if (!pendingInstitutes.Any()) return NotFound("No application pending");

                return Ok(pendingInstitutes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet("GetApplications")]
        public IActionResult GetApplications()
        {
            try
            {
                var pendingApplications = _context.ScholarshipApplications
                    .Where(app => app.ApprovedByInstitute && app.ApprovedByOfficer && !app.ApprovedByMinistry)
                    .ToList();

                if (!pendingApplications.Any()) return BadRequest("No application present");

                return Ok(pendingApplications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Route("VerifyInstitute/{id}")]
        public IActionResult VerifyInstitute(int id)
        {
            try
            {
                var pendingInstitute = _context.Institutes.Find(id);

                pendingInstitute.ApprovedByMinistery = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        [HttpPost]
        [Route("VerifyApplication/{id}")]
        public IActionResult VerifyApplication(int id)
        {
            try
            {
                var application = _context.ScholarshipApplications.Find(id);

                application.ApprovedByMinistry = true;

                var result = _context.SaveChanges() > 0;

                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }
    }
}