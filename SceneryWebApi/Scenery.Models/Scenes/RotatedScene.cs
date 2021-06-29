// <copyright file="RotatedScene.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    using System;

    /// <inheritdoc/>
    public class RotatedScene : TransformedScene
    {
        /// <summary>
        /// Gets or sets the rotation axis.
        /// </summary>
        public Vector3 Axis { get; set; } = new Vector3
        {
            XCoordinate = 0D,
            YCoordinate = 0D,
            ZCoordinate = 1D,
        };

        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        public double Angle { get; set; } = Math.PI / 2D;
    }
}
