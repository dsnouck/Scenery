// <copyright file="IVector3Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// provides operations concerning <see cref="Vector3"/>s.
/// </summary>
public interface IVector3Component
{
    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="otherVector">The other vector.</param>
    /// <returns>The sum of the vectors.</returns>
    Vector3 Add(Vector3 vector, Vector3 otherVector);

    /// <summary>
    /// Creates a vector with spherical coordinates.
    /// </summary>
    /// <param name="radius">The radius (r).</param>
    /// <param name="inclination">The inclination (θ).</param>
    /// <param name="azimuth">The azimuth (φ).</param>
    /// <returns>The vector with the spherical coordinates.</returns>
    Vector3 CreateVector3FromSphericalCoordinates(double radius, double inclination, double azimuth);

    /// <summary>
    /// Calculates the cross product between two vectors.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="otherVector">The other vector.</param>
    /// <returns>The cross product between the vectors.</returns>
    Vector3 CrossProduct(Vector3 vector, Vector3 otherVector);

    /// <summary>
    /// Divides a vector by a scalar divisor.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns>The vector divided by the divisor.</returns>
    Vector3 Divide(Vector3 vector, double divisor);

    /// <summary>
    /// Calculates the dot product between two vectors.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="otherVector">The other vector.</param>
    /// <returns>The dot product between the vectors.</returns>
    double DotProduct(Vector3 vector, Vector3 otherVector);

    /// <summary>
    /// Calculates the length of a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The length of the vector.</returns>
    double Length(Vector3 vector);

    /// <summary>
    /// Multiplies a vector with a scalar factor.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="factor">The factor.</param>
    /// <returns>The vector multiplied with the factor.</returns>
    Vector3 Multiply(Vector3 vector, double factor);

    /// <summary>
    /// Normalizes a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The normalized vector.</returns>
    Vector3 Normalize(Vector3 vector);

    /// <summary>
    /// Subtracts two vectors.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="otherVector">The other vector.</param>
    /// <returns>The difference between the vectors.</returns>
    Vector3 Subtract(Vector3 vector, Vector3 otherVector);
}
