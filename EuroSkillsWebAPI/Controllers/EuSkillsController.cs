using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EuroSkillsWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EuroSkillsWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EuSkillsController : ControllerBase
    {
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            using(var context = new euroskillsContext())
            {
                try
                {
                    return Ok(context.Versenyzos.ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("GetAll/{id}")]
        public IActionResult GetByIs(int id)
        {
            using (var context = new euroskillsContext())
            {
                try
                {
                    var result = context.Versenyzos.Where(cx => cx.Id == id).ToList();
                    if (result.Any())
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound("Nem létezik!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("GetAsNoTracking")]
        public IActionResult GetAsNoTracking()
        {
            using (var context = new euroskillsContext())
            {
                try
                {
                    return Ok(context.Versenyzos.AsNoTracking().ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("GetAllData")]
        public IActionResult GetAllData()
        {
            using (var context = new euroskillsContext())
            {
                try
                {
                    return Ok(context.Versenyzos
                        .Include(v => v.Szakma)
                        .Include(v => v.Orszag)
                        .ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("GetOrszagNev/{orszagNev}")]
        public async Task<IEnumerable<object>> GetOrszagNev(string orszagNev)
        {
            using (var context = new euroskillsContext())
            {
                var result = from o in context.Orszags join v in context.Versenyzos on o.Id equals v.OrszagId where o.OrszagNev == orszagNev select new { o.OrszagNev, v.Pont };
                try
                {
                    return await result.ToListAsync();
                }
                catch (Exception ex)
                {
                    return (IEnumerable<object>)BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("Új-Szakma")]
        public IActionResult PostNewProfession([FromBody] Szakma szakma)
        {
            using(var context = new euroskillsContext())
            {
                try
                {
                    context.Szakmas.Add(szakma);
                    context.SaveChanges();
                    return StatusCode(201, "Új szakma sikeresen felvéve!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("Új-Versenyző")]
        public IActionResult PostNewCompetitor([FromBody] Versenyzo versenyzo)
        {
            using(var context = new euroskillsContext())
            {
                try
                {
                    context.Versenyzos.Add(versenyzo);
                    context.SaveChanges();
                    return StatusCode(201, new {versenyzo.Id });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            using(var context=new euroskillsContext())
            {
                try
                {
                    var versenyzo = context.Versenyzos.FirstOrDefault(v => v.Id == id);
                    if (versenyzo != null)
                    {
                        context.Versenyzos.Remove(versenyzo);
                        context.SaveChanges();
                        return StatusCode(201, "Sikeres törlés!");
                    }
                    else
                    {
                        return NotFound("Nem létezik!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
