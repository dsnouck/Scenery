using Microsoft.AspNetCore.Mvc;
using Scenery.Models;
using Scenery.Models.Scenes;

namespace Scenery.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScenesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return base.Ok(new SceneContainer { Scene = new ScaledScene { OriginalScene = new SphereScene() } });
        }

        [HttpPost]
        public IActionResult Post(SceneContainer scene)
        {
            return base.Ok(scene);
        }
    }
}
