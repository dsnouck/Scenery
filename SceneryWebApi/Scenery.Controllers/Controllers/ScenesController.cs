using Microsoft.AspNetCore.Mvc;
using Scenery.Models;
using Scenery.Models.Scenes;
using System.Drawing;
using System.IO;

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
            using var image = GetImage();
            return this.File(image, "image/png", "scene.png");
        }

        private static Stream GetImage()
        {
            var rowCount = 240;
            var columnCount = 320;
            var bitmap = new Bitmap(columnCount, rowCount);
            
            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    bitmap.SetPixel(
                        columnIndex,
                        rowIndex,
                        System.Drawing.Color.FromArgb(
                            byte.MaxValue,
                            byte.MinValue,
                            byte.MinValue));
                }
            }

            var stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Position = 0;
            return stream;
        }
    }
}
