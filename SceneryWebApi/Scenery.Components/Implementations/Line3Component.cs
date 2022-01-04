// <copyright file="Line3Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using System;
using Scenery.Components.Interfaces;
using Scenery.Models;

/// <inheritdoc/>
public class Line3Component : ILine3Component
{
    private readonly IVector3Component vector3Component;

    /// <summary>
    /// Initializes a new instance of the <see cref="Line3Component"/> class.
    /// </summary>
    /// <param name="vector3Component">An <see cref="IVector3Component"/>.</param>
    public Line3Component(
        IVector3Component vector3Component)
    {
        this.vector3Component = vector3Component;
    }

    /// <inheritdoc/>
    public Vector3 GetPointAtDistance(Line3 line, double distance)
    {
        ArgumentNullException.ThrowIfNull(line);

        return this.vector3Component.Add(
            line.Origin,
            this.vector3Component.Multiply(
                line.Direction,
                distance));
    }
}
