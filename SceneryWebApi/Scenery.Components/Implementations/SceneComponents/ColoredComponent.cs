// <copyright file="ColoredComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class ColoredComponent : ISceneComponent
{
    private readonly ISceneComponent sceneComponent;
    private readonly Color color;

    /// <summary>
    /// Initializes a new instance of the <see cref="ColoredComponent"/> class.
    /// </summary>
    /// <param name="sceneComponent">The original scene component.</param>
    /// <param name="color">The color.</param>
    public ColoredComponent(
        ISceneComponent sceneComponent,
        Color color)
    {
        this.sceneComponent = sceneComponent;
        this.color = color;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        return this.sceneComponent.Contains(point);
    }

    /// <inheritdoc/>
    public List<SurfaceIntersection> GetAllSurfaceIntersections(Line3 lineOfSight)
    {
        return this.sceneComponent.GetAllSurfaceIntersections(lineOfSight)
            .Select(surfaceIntersection => new SurfaceIntersection
            {
                Color = this.color,
                Distance = surfaceIntersection.Distance,
                Normal = surfaceIntersection.Normal,
            })
            .ToList();
    }
}
