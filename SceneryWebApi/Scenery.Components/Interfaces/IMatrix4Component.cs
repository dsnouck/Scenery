// <copyright file="IMatrix4Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;

    /// <summary>
    /// Provides operations concerning <see cref="Matrix4"/>s.
    /// </summary>
    public interface IMatrix4Component
    {
        /// <summary>
        /// Gets the matrix for rotating <paramref name="angle"/> around <paramref name="axis"/>.
        /// </summary>
        /// <param name="axis">The rotation axis.</param>
        /// <param name="angle">The rotation angle.</param>
        /// <returns>The matrix for rotating <paramref name="angle"/> around <paramref name="axis"/>.</returns>
        Matrix4 GetRotationMatrix(Vector3 axis, double angle);

        /// <summary>
        /// Gets the matrix for scaling with <paramref name="factor"/>.
        /// </summary>
        /// <param name="factor">The scaling factor.</param>
        /// <returns>The matrix for scaling with <paramref name="factor"/>.</returns>
        Matrix4 GetScalingMatrix(double factor);

        /// <summary>
        /// Gets the matrix for translating with <paramref name="translation"/>.
        /// </summary>
        /// <param name="translation">The translation.</param>
        /// <returns>The matrix for translating with <paramref name="translation"/>.</returns>
        Matrix4 GetTranslationMatrix(Vector3 translation);

        /// <summary>
        /// Multiplies <paramref name="matrix"/> with <paramref name="vector"/>.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="vector">The vector.</param>
        /// <returns><paramref name="matrix"/> multiplied with <paramref name="vector"/>.</returns>
        Vector4 Multiply(Matrix4 matrix, Vector4 vector);
    }
}
