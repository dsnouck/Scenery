// <copyright file="UnionComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class UnionComponent : ISceneComponent
{
    private readonly ILine3Component line3Component;
    private readonly ISceneComponent sceneComponent;
    private readonly ISceneComponent otherSceneComponent;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnionComponent"/> class.
    /// </summary>
    /// <param name="line3Component">An <see cref="ILine3Component"/>.</param>
    /// <param name="sceneComponent">The original <see cref="ISceneComponent"/>.</param>
    /// <param name="otherSceneComponent">The other original <see cref="ISceneComponent"/>.</param>
    public UnionComponent(
        ILine3Component line3Component,
        ISceneComponent sceneComponent,
        ISceneComponent otherSceneComponent)
    {
        this.line3Component = line3Component;
        this.sceneComponent = sceneComponent;
        this.otherSceneComponent = otherSceneComponent;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        return this.sceneComponent.Contains(point) ||
            this.otherSceneComponent.Contains(point);
    }

    /// <inheritdoc/>
    public List<SurfaceIntersection> GetAllSurfaceIntersections(Line3 lineOfSight)
    {
        ArgumentNullException.ThrowIfNull(lineOfSight);

        var allSurfaceIntersections = new List<SurfaceIntersection>();

        var sceneSurfaceIntersections = this.sceneComponent.GetAllSurfaceIntersections(lineOfSight);
        foreach (var surfaceIntersection in sceneSurfaceIntersections)
        {
            var point = this.line3Component.GetPointAtDistance(lineOfSight, surfaceIntersection.Distance);
            if (!this.otherSceneComponent.Contains(point))
            {
                allSurfaceIntersections.Add(surfaceIntersection);
            }
        }

        var otherSceneSurfaceIntersections = this.otherSceneComponent.GetAllSurfaceIntersections(lineOfSight);
        foreach (var surfaceIntersection in otherSceneSurfaceIntersections)
        {
            var point = this.line3Component.GetPointAtDistance(lineOfSight, surfaceIntersection.Distance);
            if (!this.sceneComponent.Contains(point))
            {
                allSurfaceIntersections.Add(surfaceIntersection);
            }
        }

        return allSurfaceIntersections;
    }
}
