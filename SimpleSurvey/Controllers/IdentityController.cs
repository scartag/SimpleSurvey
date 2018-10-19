using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SimpleSurvey.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var userId = (User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value);

            return new JsonResult(from c in User.Claims select new { c.Type, c.Value , User = $"unique id: {(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value)}"});
        }

    }
}
