﻿using Microsoft.AspNetCore.Mvc;
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
            var scene = new SceneContainer
            {
                Scene = new IntersectedScene
                {
                    Scenes =
                    {
                        new CubeScene(),
                        new ScaledScene
                        {
                            Factor = 1.3D,
                            OriginalScene = new SphereScene(),
                        },
                    },
                },
            };
            return base.Ok(scene);
        }

        [HttpPost]
        public IActionResult Post(SceneContainer scene)
        {
            return base.Ok(scene);
        }
    }
}
