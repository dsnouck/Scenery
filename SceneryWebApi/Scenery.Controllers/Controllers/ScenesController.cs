using Microsoft.AspNetCore.Mvc;
using Scenery.Models;
using System.Collections.Generic;

namespace Scenery.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return base.Ok(new List<SceneContainer> { new SceneContainer() });
        }
    }
}
