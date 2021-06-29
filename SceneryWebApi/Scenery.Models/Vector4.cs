// <copyright file="Vector4.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models
{
    /// <summary>
    /// Represents a four-dimensional vector.
    /// </summary>
    public class Vector4
    {
        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        public double XCoordinate { get; set; } = 0D;

        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        public double YCoordinate { get; set; } = 0D;

        /// <summary>
        /// Gets or sets the z-coordinate.
        /// </summary>
        public double ZCoordinate { get; set; } = 0D;

        /// <summary>
        /// Gets or sets the w-coordinate.
        /// </summary>
        public double WCoordinate { get; set; } = 0D;
    }
}
