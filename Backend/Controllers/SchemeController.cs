using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Backend.DTOs.ScholarshipScheme;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemeController : ControllerBase
    {
        private readonly ScholarshipDbContext _context;

        public SchemeController(ScholarshipDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get all the scholarship schemes present in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSchemes()
        {
            try
            {
                var schemes = _context.ScholarshipSchemes.ToList();

                if (!schemes.Any()) return NotFound("No Scheme Present");

                var schemesDto = schemes.Select(x => new SchemeResponseDto
                {
                    SchemeId = x.SchemeId,
                    Name = x.Name,
                    Description = x.Description
                });

                return Ok(schemesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To get details of a scholarship schemes by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetScheme(int id)
        {
            try
            {
                var scheme = _context.ScholarshipSchemes.Find(id);

                if (scheme == null) return NotFound("Scheme not present");

                return Ok(new SchemeResponseDto
                {
                    SchemeId = scheme.SchemeId,
                    Name = scheme.Name,
                    Description = scheme.Description
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To create a new scholarship scheme. Only ministry is authorized to create new scheme
        /// </summary>
        /// <param name="scholarshipSchemeDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Ministry")]
        public IActionResult CreateScheme([FromBody] ScholarshipSchemeDto scholarshipSchemeDto)
        {
            try
            {
                var scheme = new ScholarshipScheme()
                {
                    Name = scholarshipSchemeDto.Name,
                    Description = scholarshipSchemeDto.Description
                };

                _context.ScholarshipSchemes.Add(scheme);

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Faced some problem....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To edit an existing scholarship scheme. Only ministry is authorized to edit the schemes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scholarshipSchemeDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult EditScheme(int id, [FromBody] ScholarshipSchemeDto scholarshipSchemeDto)
        {
            try
            {
                var scheme = _context.ScholarshipSchemes.Find(id);

                scheme.Name = scholarshipSchemeDto.Name;
                scheme.Description = scholarshipSchemeDto.Description;

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    return Ok(new { Result = result });
                }
                else
                {
                    return BadRequest("Faced some problem....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// To delete an existing scholarship scheme. Only ministry is authorizes to delete schemes
        /// from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Ministry")]
        public IActionResult DeleteScheme(int id)
        {
            try
            {
                var scheme = _context.ScholarshipSchemes.Find(id);

                _context.ScholarshipSchemes.Remove(scheme);

                var result = _context.SaveChanges() > 0;

                if (result)
                {
                    var schemes = _context.ScholarshipSchemes.ToList();

                    return Ok(schemes);
                }
                else
                {
                    return BadRequest("Faced some problem....");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
