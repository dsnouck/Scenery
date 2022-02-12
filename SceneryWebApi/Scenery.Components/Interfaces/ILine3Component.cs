// <copyright file="ILine3Component.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces;

using Scenery.Models;

/// <summary>
/// Provides operations concerning <see cref="Line3"/>s.
/// </summary>
public interface ILine3Component
{
    /// <summary>
    /// Gets a point along a line.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <param name="distance">The distance.</param>
    /// <returns>The point along the line at the distance.</returns>
    Vector3 GetPointAtDistance(Line3 line, double distance);
}
