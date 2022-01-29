// <copyright file="ISceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces.SceneComponents;

using Scenery.Models;

/// <summary>
/// Provides the operations necessary for projecting a scene.
/// </summary>
public interface ISceneComponent
{
    /// <summary>
    /// Calculates whether a point is inside the scene.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>Whether the point is inside the scene.</returns>
    bool Contains(Vector3 point);

    /// <summary>
    /// Calculates all intercepts with the scene along a line of sight.
    /// </summary>
    /// <param name="lineOfSight">The line of sight.</param>
    /// <returns>All intercepts with the scene along the line of sight.</returns>
    List<Intercept> GetAllIntercepts(Line3 lineOfSight);
}
