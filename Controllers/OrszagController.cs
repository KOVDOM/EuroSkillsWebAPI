using Kovács_Dominik_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kovács_Dominik_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        [HttpGet("EuSkills/osszesOrszagSzama")]
        public IActionResult osszesOrszagSzama()
        {
            using (var context = new euroskillsContext())
            {
                try
                {
                    return Ok("összes ország száma: " + context.Orszags.Count());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
