// <copyright file="ISceneContainerComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides operations concerning <see cref="SceneContainer"/>s.
/// </summary>
public interface ISceneContainerComponent
{
    /// <summary>
    /// Gets examples of <see cref="SceneContainer"/>s.
    /// </summary>
    /// <returns>Examples of <see cref="SceneContainer"/>s.</returns>
    Dictionary<string, SceneContainer> GetExamples();

    /// <summary>
    /// Renders a scene container to an image.
    /// Ties together all the necessary steps: projection, sampling and creating a bitmap image.
    /// </summary>
    /// <param name="sceneContainer">The scene container.</param>
    /// <returns>The scene container rendered to an image.</returns>
    Stream Render(SceneContainer sceneContainer);
}
