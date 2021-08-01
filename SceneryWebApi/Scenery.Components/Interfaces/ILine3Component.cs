// <copyright file="ILine3Component.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Components.Interfaces
{
    using Scenery.Models;

    /// <summary>
    /// Provides operations concerning <see cref="Line3"/>s.
    /// </summary>
    public interface ILine3Component
    {
        /// <summary>
        /// Gets the point along the <paramref name="line"/> at the given <paramref name="distance"/>.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="distance">The distance.</param>
        /// <returns>The point along the <paramref name="line"/> at the given <paramref name="distance"/>.</returns>
        Vector3 GetPointAtDistance(Line3 line, double distance);
    }
}
