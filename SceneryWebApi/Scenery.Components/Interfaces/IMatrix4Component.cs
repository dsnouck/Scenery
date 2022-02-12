// <copyright file="IMatrix4Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides operations concerning <see cref="Matrix4"/>s.
/// </summary>
public interface IMatrix4Component
{
    /// <summary>
    /// Creates a rotation matrix.
    /// </summary>
    /// <param name="axis">The rotation axis.</param>
    /// <param name="angle">The rotation angle.</param>
    /// <returns>The rotation matrix for the rotation axis and angle.</returns>
    Matrix4 CreateRotationMatrix(Vector3 axis, double angle);

    /// <summary>
    /// Creates a scaling matrix.
    /// </summary>
    /// <param name="factor">The scaling factor.</param>
    /// <returns>The scaling matrix for the scaling factor.</returns>
    Matrix4 CreateScalingMatrix(double factor);

    /// <summary>
    /// Creates a translation matrix.
    /// </summary>
    /// <param name="translation">The translation.</param>
    /// <returns>The translation matrix for the translation.</returns>
    Matrix4 CreateTranslationMatrix(Vector3 translation);

    /// <summary>
    /// Multiplies a matrix with a vector.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="vector">The vector.</param>
    /// <returns>The product of the matrix and the vector.</returns>
    Vector4 Multiply(Matrix4 matrix, Vector4 vector);
}
