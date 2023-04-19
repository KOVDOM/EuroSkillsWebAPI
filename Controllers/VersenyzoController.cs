using Kovács_Dominik_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kovács_Dominik_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersenyzoController : ControllerBase
    {
        [HttpGet("EuSkills/GetVersenyzok")]
        public IActionResult getVersenyzok()
        {
            using(var context=new euroskillsContext())
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

        [HttpGet("EuSkills/getVersenyzo/{id}")]
        public IActionResult getVersenyzo(int id) 
        {
            using(var context=new euroskillsContext())
            {
                try
                {
                    return Ok(context.Versenyzos.Where(cx => cx.Id == id).ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("EuSkills/addVersenyzo")]
        public IActionResult addVersenyzo([FromBody] Versenyzo versenyzo)
        {
            using(var context=new euroskillsContext())
            {
                try
                {
                    context.Versenyzos.Add(versenyzo);
                    context.SaveChanges();
                    return Ok("Versenyző hozzáadása sikeresen megtörtént!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
