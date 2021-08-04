// <copyright file="ScenesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Scenery.Components.Interfaces;
    using Scenery.Models;
    using Scenery.Models.Scenes;

    /// <summary>
    /// A controller for scenes.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ScenesController : ControllerBase
    {
        private readonly ISceneContainerComponent sceneContainerComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScenesController"/> class.
        /// </summary>
        /// <param name="sceneContainerComponent">An <see cref="ISceneContainerComponent"/>.</param>
        public ScenesController(ISceneContainerComponent sceneContainerComponent)
        {
            this.sceneContainerComponent = sceneContainerComponent;
        }

        /// <summary>
        /// Gets an example of a scene.
        /// </summary>
        /// <returns>An example of a scene.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var scene = new SceneContainer
            {
                Scene = new IntersectedScene
                {
                    Scenes =
                    {
                        new ColoredScene
                        {
                            Color = new Color
                            {
                                RedComponent = 1D,
                                GreenComponent = 0D,
                                BlueComponent = 0D,
                            },
                            OriginalScene = new CubeScene(),
                        },
                        new ColoredScene
                        {
                            Color = new Color
                            {
                                RedComponent = 0D,
                                GreenComponent = 0D,
                                BlueComponent = 1D,
                            },
                            OriginalScene = new InvertedScene
                            {
                                OriginalScene = new ScaledScene
                                {
                                    Factor = 1.3D,
                                    OriginalScene = new SphereScene(),
                                },
                            },
                        },
                    },
                },
                ProjectorSettings = new ProjectorSettings
                {
                    Eye = new Vector3
                    {
                        XCoordinate = 4.430761575624772D,
                        YCoordinate = -2.4205360806680094D,
                        ZCoordinate = 3.2418138352088386D,
                    },
                    Focus = new Vector3
                    {
                        XCoordinate = 0D,
                        YCoordinate = 0D,
                        ZCoordinate = 0D,
                    },
                    HorizontalOpeningAngle = 0.7853981633974483D,
                    BackgroundColor = new Color
                    {
                        RedComponent = 1D,
                        GreenComponent = 1D,
                        BlueComponent = 1D,
                    },
                },
                SamplerSettings = new SamplerSettings
                {
                    ColumnCount = 160,
                    RowCount = 120,
                    SubsampleCount = 2,
                },
            };

            return this.Ok(scene);
        }

        /// <summary>
        /// Renders the scene to an image.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <returns>The scene rendered to an image.</returns>
        [HttpPost]
        public IActionResult Post(SceneContainer scene)
        {
            var image = this.sceneContainerComponent.GetStream(scene);
            return this.File(image, "image/png", "scene.png");
        }
    }
}
