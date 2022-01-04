// <copyright file="SphereComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using System;
using System.Collections.Generic;
using System.Linq;
using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class SphereComponent : ISceneComponent
{
    private readonly IFuncDoubleDoubleComponent funcDoubleDoubleComponent;
    private readonly ILine3Component line3Component;
    private readonly IVector3Component vector3Component;

    /// <summary>
    /// Initializes a new instance of the <see cref="SphereComponent"/> class.
    /// </summary>
    /// <param name="funcDoubleDoubleComponent">An <see cref="IFuncDoubleDoubleComponent"/>.</param>
    /// <param name="line3Component">An <see cref="ILine3Component"/>.</param>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public SphereComponent(
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
        return this.vector3Component.DotProduct(point, point) <= 1D;
    }

    /// <inheritdoc/>
    public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
    {
        ArgumentNullException.ThrowIfNull(lineOfSight);

        // These are the coefficients of the quadratic equation x ↦ ax² + bx + c we want to solve.
        var a = this.vector3Component.DotProduct(
            lineOfSight.Direction,
            lineOfSight.Direction);
        var b = this.vector3Component.DotProduct(
            lineOfSight.Direction,
            lineOfSight.Origin)
            * 2D;
        var c = this.vector3Component.DotProduct(
            lineOfSight.Origin,
            lineOfSight.Origin)
            - 1D;

        var zeros = this.funcDoubleDoubleComponent.GetRealZerosOfQuadraticFunction(a, b, c);

        return zeros.
            Select(zero => new Intercept
            {
                Distance = zero,
                Normal = () => this.vector3Component.Multiply(
                    this.vector3Component.Normalize(
                        this.line3Component.GetPointAtDistance(lineOfSight, zero)),
                    this.vector3Component.GetLength(lineOfSight.Direction)),
            })
            .ToList();
    }
}
