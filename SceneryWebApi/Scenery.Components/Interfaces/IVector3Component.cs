// <copyright file="IVector3Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;

    /// <summary>
    /// provides operations concerning <see cref="Vector3"/>s.
    /// </summary>
    public interface IVector3Component
    {
        /// <summary>
        /// Adds <paramref name="otherVector"/> to <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="otherVector">The other vector.</param>
        /// <returns><paramref name="otherVector"/> added to <paramref name="vector"/>.</returns>
        Vector3 Add(Vector3 vector, Vector3 otherVector);

        /// <summary>
        /// Creates a <see cref="Vector3"/> from spherical coordinates.
        /// </summary>
        /// <param name="radius">The radius (r).</param>
        /// <param name="inclination">The inclination (θ).</param>
        /// <param name="azimuth">The azimuth (φ).</param>
        /// <returns>The <see cref="Vector3"/> with the given spherical coordinates.</returns>
        Vector3 CreateVector3FromSphericalCoordinates(double radius, double inclination, double azimuth);

        /// <summary>
        /// Calculates the cross product between <paramref name="vector"/> and <paramref name="otherVector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="otherVector">The other vector.</param>
        /// <returns>The cross product between <paramref name="vector"/> and <paramref name="otherVector"/>.</returns>
        Vector3 CrossProduct(Vector3 vector, Vector3 otherVector);

        /// <summary>
        /// Divides <paramref name="vector"/> by <paramref name="divisor"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns><paramref name="vector"/> divided by <paramref name="divisor"/>.</returns>
        Vector3 Divide(Vector3 vector, double divisor);

        /// <summary>
        /// Calculates the dot product between <paramref name="vector"/> and <paramref name="otherVector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="otherVector">The other vector.</param>
        /// <returns>The dot product between <paramref name="vector"/> and <paramref name="otherVector"/>.</returns>
        double DotProduct(Vector3 vector, Vector3 otherVector);

        /// <summary>
        /// Calculates the length of <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The length of <paramref name="vector"/>.</returns>
        double GetLength(Vector3 vector);

        /// <summary>
        /// Multiplies <paramref name="vector"/> with <paramref name="factor"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="factor">The factor.</param>
        /// <returns><paramref name="vector"/> multiplied with <paramref name="factor"/>.</returns>
        Vector3 Multiply(Vector3 vector, double factor);

        /// <summary>
        /// Normalizes <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><paramref name="vector"/> normalized.</returns>
        Vector3 Normalize(Vector3 vector);

        /// <summary>
        /// Subtracts <paramref name="otherVector"/> from <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="otherVector">The other vector.</param>
        /// <returns><paramref name="otherVector"/> subtracted from <paramref name="vector"/>.</returns>
        Vector3 Subtract(Vector3 vector, Vector3 otherVector);
    }
}
