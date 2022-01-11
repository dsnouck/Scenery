﻿// <copyright file="IFuncDoubleDoubleComponent.cs" company="dsnouck">
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
    /// Gets the line through two points.
    /// </summary>
    /// <param name="point">A point.</param>
    /// <param name="otherPoint">Another point.</param>
    /// <returns>The line through two points.</returns>
    Func<double, double> GetLineThrough(Vector2 point, Vector2 otherPoint);

    /// <summary>
    /// Gets the real zeros of x ↦ ax² + bx + c.
    /// </summary>
    /// <param name="a">The quadratic coefficient.</param>
    /// <param name="b">The linear coefficient.</param>
    /// <param name="c">The constant coefficient.</param>
    /// <returns>The real zeros of x ↦ ax² + bx + c.</returns>
    List<double> GetRealZerosOfQuadraticFunction(double a, double b, double c);
}
