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

        [HttpGet]
        public IActionResult GetSchemes()
        {
            try
            {
                var schemes = _context.ScholarshipSchemes.ToList();

                if (!schemes.Any())
                {
                    return NotFound("No Scheme Present");
                }

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

        [HttpGet("{id}")]
        public IActionResult GetScheme(int id)
        {
            try
            {
                var scheme = _context.ScholarshipSchemes.Find(id);

                if (scheme == null)
                {
                    return NotFound("Scheme not present");
                }

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

        [HttpPost]
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
                } else
                {
                    return BadRequest("Faced some problem....");
                }
            } 
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScheme(int id)
        {
            try
            {
                var scheme = _context.ScholarshipSchemes.Find(id);
                _context.ScholarshipSchemes.Remove(scheme);

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
    }
}
