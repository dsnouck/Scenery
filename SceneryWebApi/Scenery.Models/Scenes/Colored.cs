// <copyright file="Colored.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
    public class Colored : Transformed
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; } = new Color
        {
            R = 1D,
            G = 0D,
            B = 0D,
        };
    }
}
