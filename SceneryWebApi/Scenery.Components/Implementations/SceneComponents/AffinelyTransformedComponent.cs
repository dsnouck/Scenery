// <copyright file="AffinelyTransformedComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations.SceneComponents;

using Scenery.Components.Interfaces;
using Scenery.Components.Interfaces.SceneComponents;
using Scenery.Models;

/// <inheritdoc/>
public class AffinelyTransformedComponent : ISceneComponent
{
    private readonly IMatrix4Component matrix4Component;
    private readonly ISceneComponent sceneComponent;
    private readonly Matrix4 transformation;
    private readonly Matrix4 backwardTransformation;

    /// <summary>
    /// Initializes a new instance of the <see cref="AffinelyTransformedComponent"/> class.
    /// </summary>
    /// <param name="matrix4Component">An <see cref="IMatrix4Component"/>.</param>
    /// <param name="sceneComponent">The original <see cref="ISceneComponent"/>.</param>
    /// <param name="transformation">The forward transformation matrix.</param>
    /// <param name="backwardTransformation">The backward transformation matrix.</param>
    public AffinelyTransformedComponent(
        IMatrix4Component matrix4Component,
        ISceneComponent sceneComponent,
        Matrix4 transformation,
        Matrix4 backwardTransformation)
    {
        this.matrix4Component = matrix4Component;
        this.sceneComponent = sceneComponent;
        this.transformation = transformation;
        this.backwardTransformation = backwardTransformation;
    }

    /// <inheritdoc/>
    public bool Contains(Vector3 point)
    {
        ArgumentNullException.ThrowIfNull(point);

        return this.sceneComponent.Contains(this.TransformedBackPoint(point));
    }

    /// <inheritdoc/>
    public List<Intercept> GetAllIntercepts(Line3 lineOfSight)
    {
        ArgumentNullException.ThrowIfNull(lineOfSight);

        var transformedLineOfSight = new Line3
        {
            Origin = this.TransformedBackPoint(lineOfSight.Origin),
            Direction = this.TransformedBackDirection(lineOfSight.Direction),
        };

        return this.sceneComponent.GetAllIntercepts(transformedLineOfSight)
            .Select(intercept => new Intercept
            {
                Color = intercept.Color,
                Distance = intercept.Distance,
                Normal = () => this.TransformedDirection(intercept.Normal()),
            })
            .ToList();
    }

    private Vector3 TransformedDirection(Vector3 direction)
    {
        var direction4 = new Vector4
        {
            X = direction.X,
            Y = direction.Y,
            Z = direction.Z,
            W = 0D,
        };
        var transformedDirection4 = this.matrix4Component.Multiply(this.transformation, direction4);

        return new Vector3
        {
            X = transformedDirection4.X,
            Y = transformedDirection4.Y,
            Z = transformedDirection4.Z,
        };
    }

    private Vector3 TransformedBackDirection(Vector3 direction)
    {
        var direction4 = new Vector4
        {
            X = direction.X,
            Y = direction.Y,
            Z = direction.Z,
            W = 0D,
        };
        var transformedDirection4 = this.matrix4Component.Multiply(this.backwardTransformation, direction4);

        return new Vector3
        {
            X = transformedDirection4.X,
            Y = transformedDirection4.Y,
            Z = transformedDirection4.Z,
        };
    }

    private Vector3 TransformedBackPoint(Vector3 point)
    {
        var point4 = new Vector4
        {
            X = point.X,
            Y = point.Y,
            Z = point.Z,
            W = 1D,
        };
        var transformedPoint4 = this.matrix4Component.Multiply(this.backwardTransformation, point4);

        return new Vector3
        {
            X = transformedPoint4.X,
            Y = transformedPoint4.Y,
            Z = transformedPoint4.Z,
        };
    }
}
