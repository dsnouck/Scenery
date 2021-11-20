// <copyright file="Transformed.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
    public class Transformed : Scene
    {
        /// <summary>
        /// Gets or sets the original scene.
        /// </summary>
        public Scene Scene { get; set; } = new Icosahedron();
    }
}
