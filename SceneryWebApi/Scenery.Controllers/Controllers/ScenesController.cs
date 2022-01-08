// <copyright file="ScenesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Controllers;

using Microsoft.AspNetCore.Mvc;
using Scenery.Components.Interfaces;
using Scenery.Models;

// TODO: Use the new minimal hosting model.

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
    /// Gets examples of scenes.
    /// </summary>
    /// <returns>Examples of scenes.</returns>
    [HttpGet]
    public IActionResult Get()
    {
        var scenes = this.sceneContainerComponent.GetExamples();
        return this.Ok(scenes);
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
