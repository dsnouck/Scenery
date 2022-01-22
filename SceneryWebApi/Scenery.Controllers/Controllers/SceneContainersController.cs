// <copyright file="SceneContainersController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Controllers.Controllers;

using Microsoft.AspNetCore.Mvc;
using Scenery.Components.Interfaces;
using Scenery.Models;

/// <summary>
/// A controller for scene containers.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SceneContainersController : ControllerBase
{
    private readonly ISceneContainerComponent sceneContainerComponent;

    /// <summary>
    /// Initializes a new instance of the <see cref="SceneContainersController"/> class.
    /// </summary>
    /// <param name="sceneContainerComponent">An <see cref="ISceneContainerComponent"/>.</param>
    public SceneContainersController(ISceneContainerComponent sceneContainerComponent)
    {
        this.sceneContainerComponent = sceneContainerComponent;
    }

    /// <summary>
    /// Gets some examples of scene containers.
    /// </summary>
    /// <returns>Some examples of scene containers.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetExamples()
    {
        var examples = this.sceneContainerComponent.GetExamples();
        return this.Ok(examples);
    }

    /// <summary>
    /// Renders a scene container to an image.
    /// </summary>
    /// <param name="sceneContainer">The scene container.</param>
    /// <returns>The scene container rendered to an image.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult PostSceneContainer(SceneContainer sceneContainer)
    {
        var image = this.sceneContainerComponent.GetImage(sceneContainer);
        return this.File(image, "image/png", "scene.png");
    }
}
