// <copyright file="Rotated.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    using System;

    /// <inheritdoc/>
    public class Rotated : Transformed
    {
        /// <summary>
        /// Gets or sets the rotation axis.
        /// </summary>
        public Vector3 Axis { get; set; } = new Vector3
        {
            X = 0D,
            Y = 0D,
            Z = 1D,
        };

        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        public double Angle { get; set; } = Math.PI / 2D;
    }
}
