// <copyright file="CylinderComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class CylinderComponent : ISceneComponent
{
    private readonly IFuncDoubleDoubleComponent funcDoubleDoubleComponent;
    private readonly ILine3Component line3Component;
    private readonly IVector3Component vector3Component;

    /// <summary>
    /// Initializes a new instance of the <see cref="CylinderComponent"/> class.
    /// </summary>
    /// <param name="funcDoubleDoubleComponent">An <see cref="IFuncDoubleDoubleComponent"/>.</param>
    /// <param name="line3Component">An <see cref="ILine3Component"/>.</param>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public CylinderComponent(
        IFuncDoubleDoubleComponent funcDoubleDoubleComponent,
        ILine3Component line3Component,
        IVector3Component vector3Component)
    {
        this.funcDoubleDoubleComponent = funcDoubleDoubleComponent;
        this.line3Component = line3Component;
        this.vector3Component = vector3Component;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        ArgumentNullException.ThrowIfNull(point);

        var horizontalPoint = new Vector3
        {
            X = point.X,
            Y = point.Y,
            Z = 0D,
        };

        return this.vector3Component.DotProduct(horizontalPoint, horizontalPoint) <= 1D;
    }

    /// <inheritdoc/>
    public List<SurfaceIntersection> GetAllSurfaceIntersections(Line3 lineOfSight)
    {
        ArgumentNullException.ThrowIfNull(lineOfSight);

        var horizontalLineOfSight = new Line3
        {
            Origin = new Vector3
            {
                X = lineOfSight.Origin.X,
                Y = lineOfSight.Origin.Y,
                Z = 0D,
            },
            Direction = new Vector3
            {
                X = lineOfSight.Direction.X,
                Y = lineOfSight.Direction.Y,
                Z = 0D,
            },
        };

        // These are the coefficients of the quadratic equation x ↦ ax² + bx + c we want to solve.
        var a = this.vector3Component.DotProduct(
            horizontalLineOfSight.Direction,
            horizontalLineOfSight.Direction);
        var b = this.vector3Component.DotProduct(
            horizontalLineOfSight.Direction,
            horizontalLineOfSight.Origin)
            * 2D;
        var c = this.vector3Component.DotProduct(
            horizontalLineOfSight.Origin,
            horizontalLineOfSight.Origin)
            - 1D;

        var zeros = this.funcDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(a, b, c);

        return zeros
            .Select(zero => new SurfaceIntersection
            {
                Distance = zero,
                Normal = () =>
                {
                    var surfaceIntersection = this.line3Component.GetPointAtDistance(lineOfSight, zero);
                    var horizontalSurfaceIntersection = new Vector3
                    {
                        X = surfaceIntersection.X,
                        Y = surfaceIntersection.Y,
                        Z = 0D,
                    };

                    return this.vector3Component.Multiply(
                        this.vector3Component.Normalize(horizontalSurfaceIntersection),
                        this.vector3Component.Length(lineOfSight.Direction));
                },
            })
            .ToList();
    }
}
