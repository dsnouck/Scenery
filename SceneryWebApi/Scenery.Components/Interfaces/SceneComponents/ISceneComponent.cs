// <copyright file="ISceneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces.SceneComponents;

using Scenery.Models;
using Scenery.Models.Scenes;

/// <summary>
/// Provides operations concerning <see cref="Scene"/>s.
/// </summary>
public interface ISceneComponent
{
    /// <summary>
    /// Determines whether the point is contained within the <see cref="Scene"/>.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>Whether the point contained within the <see cref="Scene"/>.</returns>
    bool Contains(Vector3 point);

    /// <summary>
    /// Gets all <see cref="Intercept"/>s along <paramref name="lineOfSight"/>.
    /// </summary>
    /// <param name="lineOfSight">The line of sight.</param>
    /// <returns>All <see cref="Intercept"/>s along <paramref name="lineOfSight"/>.</returns>
    List<Intercept> GetAllIntercepts(Line3 lineOfSight);
}
