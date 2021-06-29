// <copyright file="ProjectorSettings.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models
{
    using System;

    /// <summary>
    /// Contains projector settings.
    /// </summary>
    public class ProjectorSettings
    {
        /// <summary>
        /// Gets or sets the eye, i.e. the three-dimensional point we are looking from.
        /// </summary>
        public Vector3 Eye { get; set; } = new Vector3
        {
            XCoordinate = 2D,
            YCoordinate = 2D,
            ZCoordinate = 2D,
        };

        /// <summary>
        /// Gets or sets the focus, i.e. the three-dimensional point we are looking at.
        /// </summary>
        public Vector3 Focus { get; set; } = new Vector3();

        /// <summary>
        /// Gets or sets the horizontal opening angle.
        /// </summary>
        public double HorizontalOpeningAngle { get; set; } = Math.PI / 4D;

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public Color BackgroundColor { get; set; } = new Color();
    }
}
