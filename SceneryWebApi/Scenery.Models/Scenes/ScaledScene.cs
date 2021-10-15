// <copyright file="ScaledScene.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
    public class ScaledScene : TransformedScene
    {
        /// <summary>
        /// Gets or sets the scaling factor.
        /// </summary>
        public double Factor { get; set; } = 0.5D;
    }
}
