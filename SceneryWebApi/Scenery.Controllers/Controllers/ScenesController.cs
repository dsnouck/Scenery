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
            var scene = new SceneContainer
            {
                Scene = new IntersectedScene
                {
                    Scenes =
                    {
                        new UnitedScene
                        {
                            Scenes =
                            {
                                new ConeScene(),
                                new CubeScene(),
                            },
                        },
                        new ColoredScene
                        {
                            OriginalScene = new CylinderScene(),
                        },
                        new InvertedScene
                        {
                            OriginalScene = new DodecahedronScene(),
                        },
                        new InvisibleScene
                        {
                            OriginalScene = new EmptyScene(),
                        },
                        new PlaneScene(),
                        new RotatedScene
                        {
                            OriginalScene = new FullScene(),
                        },
                        new ScaledScene
                        {
                            OriginalScene = new IcosahedronScene(),
                        },
                        new TranslatedScene
                        {
                            OriginalScene = new OctahedronScene(),
                        },
                        new SphereScene(),
                        new TetrahedronScene(),
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
