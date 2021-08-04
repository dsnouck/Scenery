// <copyright file="ColoredScene.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
    public class ColoredScene : TransformedScene
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; } = new Color
        {
            RedComponent = 1D,
            GreenComponent = 0D,
            BlueComponent = 0D,
        };
    }
}
