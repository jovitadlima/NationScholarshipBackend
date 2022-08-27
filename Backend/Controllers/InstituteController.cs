using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteController : ControllerBase
    {
        private ScholarshipDbContext _context;
        public InstituteController(ScholarshipDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult PostRegister([FromBody] Institute _institute)
        {
            try
            {
                var institute = new Institute()
                {
                    InstituteCategory = _institute.InstituteCategory,
                    State = _institute.State,
                    District = _institute.District,
                    InstituteName = _institute.InstituteName,
                    InstituteCode = _institute.InstituteCode,
                    DiseCode = _institute.DiseCode,
                    Location = _institute.Location,
                    InstituteType = _institute.InstituteType,
                    AffliatedUniversityState = _institute.AffliatedUniversityState,
                    AffliatedUniversityName = _institute.AffliatedUniversityName,
                    YearOfAddmission = _institute.YearOfAddmission,
                    AddressLine1 = _institute.AddressLine1,
                    AddressLine2 = _institute.AddressLine2,
                    AddressCity = _institute.AddressCity,
                    AddressState = _institute.AddressState,
                    AddressDistrict = _institute.AddressDistrict,
                    AddressPincode = _institute.AddressPincode,
                    PrincipalName = _institute.PrincipalName,
                    MobileNumber = _institute.MobileNumber,
                    Telephone = _institute.Telephone
                };

                _context.Institutes.Add(institute);

                var result = _context.SaveChanges() > 0;
                if (result)
                {
                    return Ok(result);
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

        [HttpGet]
        [Route("GetAllStudents/{id}")]
        // all the students in a institute
        public IActionResult GetAllStudents(int id)
        {
            try
            {
                var students = _context.Students.Where(student => student.InstituteId == id).ToList();

                if (students.Count() == 0)
                    return NotFound("No Students");

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("GetAllApplications/{id}")]
        public IActionResult GetAllApplications(int id)
        {
            try
            {
                var allapplications = _context.ScholarshipApplications.Include("StudentDocument").ToList();
                // var allapplications = _context.ScholarshipApplications.ToList();
                var institute = _context.Institutes.Where(i => i.InstituteId == id).FirstOrDefault();
                var applications = allapplications.Where(app => app.InstituteCode == institute.InstituteCode);
                if (applications.Count() == 0)
                    return NotFound("No Applications");
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetInstitute(int id)
        {
            try
            {
                var institute = _context.Institutes.Find(id);
                return Ok(institute);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CheckRegistrationStatus/{id}")]
        public IActionResult GetStatus(int id)
        {
            try
            {
                var InstituteData = _context.Institutes.Find(id);
                var officerstatus = InstituteData.ApprovedByOfficer;
                var ministrystatus = InstituteData.ApprovedByMinistery;
                if (officerstatus && ministrystatus)
                    return Ok("Status Approved");
                else
                    return Ok("Status Pending");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("VerifyApplication/{id}")]
        public IActionResult PostVerifyApplication(int id, [FromBody] MinistryApprovalDto ministryApprovalDto)
        {
            try
            {
                var application = _context.ScholarshipApplications.Where(app => app.ApplicationId == id).FirstOrDefault();
                if (application == null)
                    return NotFound("No such application");

                application.ApprovedByInstitute = ministryApprovalDto.Approval;

                if (application.ApprovedByInstitute)
                {
                    application.CertificateUrl = ministryApprovalDto.Url;
                    _context.SaveChanges();
                    return Ok("Approved");
                }
                else
                {
                    return Ok("Pending");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }
    }
}
