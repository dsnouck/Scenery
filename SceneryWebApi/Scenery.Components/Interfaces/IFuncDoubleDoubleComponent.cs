// <copyright file="IFuncDoubleDoubleComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides operations concerning <see cref="Func{Double,Double}"/>s.
/// </summary>
public interface IFuncDoubleDoubleComponent
{
    /// <summary>
    /// Creates a line through two points.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="otherPoint">The other point.</param>
    /// <returns>The line through both points.</returns>
    Func<double, double> CreateLineThrough(Vector2 point, Vector2 otherPoint);

    /// <summary>
    /// Calculates the real zeros of x ↦ ax² + bx + c.
    /// </summary>
    /// <param name="a">The quadratic coefficient.</param>
    /// <param name="b">The linear coefficient.</param>
    /// <param name="c">The constant coefficient.</param>
    /// <returns>The real zeros of x ↦ ax² + bx + c.</returns>
    List<double> GetRealZerosOfQuadraticFunction(double a, double b, double c);
}
