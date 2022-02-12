// <copyright file="PlaneComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class PlaneComponent : ISceneComponent
{
    private readonly IVector3Component vector3Component;
    private readonly Vector3 normal;
    private readonly double epsilon;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaneComponent"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    /// <param name="normal">The normal.</param>
    public PlaneComponent(
        IVector3Component vector3Component,
        Vector3 normal)
    {
        this.vector3Component = vector3Component;
        this.normal = normal;
        this.epsilon = 0.001D;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        return this.vector3Component.DotProduct(point, this.normal) <=
            this.vector3Component.DotProduct(this.normal, this.normal);
    }

    /// <inheritdoc/>
    public List<SurfaceIntersection> GetAllSurfaceIntersections(Line3 lineOfSight)
    {
        ArgumentNullException.ThrowIfNull(lineOfSight);

        var dotProductNormalDirection = this.vector3Component.DotProduct(
            this.normal,
            lineOfSight.Direction);

        if (Math.Abs(dotProductNormalDirection) < this.epsilon)
        {
            // The line of sight is approximately parallel to the plane.
            return new List<SurfaceIntersection>();
        }

        var distance = this.vector3Component.DotProduct(
            this.normal,
            this.vector3Component.Subtract(
                this.normal,
                lineOfSight.Origin))
            / dotProductNormalDirection;

        return new List<SurfaceIntersection>
            {
                new SurfaceIntersection()
                {
                    Distance = distance,
                    Normal = () => this.vector3Component.Multiply(
                        this.vector3Component.Normalize(this.normal),
                        this.vector3Component.Length(lineOfSight.Direction)),
                },
            };
    }
}
