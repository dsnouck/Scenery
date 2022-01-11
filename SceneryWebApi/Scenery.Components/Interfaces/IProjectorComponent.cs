// <copyright file="IProjectorComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;
using Scenery.Models.Scenes;

/// <summary>
/// Provides projection operations.
/// </summary>
public interface IProjectorComponent
{
    /// <summary>
    /// Creates the two-dimensional projection of <paramref name="scene"/>.
    /// </summary>
    /// <param name="scene">The scene.</param>
    /// <param name="projectorSettings">The projector settings.</param>
    /// <returns>The two-dimensional projection of <paramref name="scene"/>.</returns>
    Func<Vector2, Color> ProjectSceneToImage(Scene scene, ProjectorSettings projectorSettings);
}
