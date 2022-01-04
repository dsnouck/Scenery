// <copyright file="Vector3Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Implementations;

using System;
using Scenery.Components.Interfaces;
using Scenery.Models;

/// <inheritdoc/>
public class Vector3Component : IVector3Component
{
    /// <inheritdoc/>
    public Vector3 Add(Vector3 vector, Vector3 otherVector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        ArgumentNullException.ThrowIfNull(otherVector);

        return new Vector3
        {
            X = vector.X + otherVector.X,
            Y = vector.Y + otherVector.Y,
            Z = vector.Z + otherVector.Z,
        };
    }

    /// <inheritdoc/>
    public Vector3 CreateVector3FromSphericalCoordinates(double radius, double inclination, double azimuth)
    {
        var sineOfInclination = Math.Sin(inclination);
        var cosineOfInclination = Math.Cos(inclination);
        var sineOfAzimuth = Math.Sin(azimuth);
        var cosineOfAzimuth = Math.Cos(azimuth);

        return new Vector3
        {
            X = radius * sineOfInclination * cosineOfAzimuth,
            Y = radius * sineOfInclination * sineOfAzimuth,
            Z = radius * cosineOfInclination,
        };
    }

    /// <inheritdoc/>
    public Vector3 CrossProduct(Vector3 vector, Vector3 otherVector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        ArgumentNullException.ThrowIfNull(otherVector);

        return new Vector3
        {
            X = (vector.Y * otherVector.Z) - (vector.Z * otherVector.Y),
            Y = (vector.Z * otherVector.X) - (vector.X * otherVector.Z),
            Z = (vector.X * otherVector.Y) - (vector.Y * otherVector.X),
        };
    }

    /// <inheritdoc/>
    public Vector3 Divide(Vector3 vector, double divisor)
    {
        return this.Multiply(vector, 1D / divisor);
    }

    /// <inheritdoc/>
    public double DotProduct(Vector3 vector, Vector3 otherVector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        ArgumentNullException.ThrowIfNull(otherVector);

        return
            (vector.X * otherVector.X) +
            (vector.Y * otherVector.Y) +
            (vector.Z * otherVector.Z);
    }

    /// <inheritdoc/>
    public double GetLength(Vector3 vector)
    {
        return Math.Sqrt(this.DotProduct(vector, vector));
    }

    /// <inheritdoc/>
    public Vector3 Multiply(Vector3 vector, double factor)
    {
        ArgumentNullException.ThrowIfNull(vector);

        return new Vector3
        {
            X = factor * vector.X,
            Y = factor * vector.Y,
            Z = factor * vector.Z,
        };
    }

    /// <inheritdoc/>
    public Vector3 Normalize(Vector3 vector)
    {
        return this.Divide(vector, this.GetLength(vector));
    }

    /// <inheritdoc/>
    public Vector3 Subtract(Vector3 vector, Vector3 otherVector)
    {
        ArgumentNullException.ThrowIfNull(vector);
        ArgumentNullException.ThrowIfNull(otherVector);

        return new Vector3
        {
            X = vector.X - otherVector.X,
            Y = vector.Y - otherVector.Y,
            Z = vector.Z - otherVector.Z,
        };
    }
}
