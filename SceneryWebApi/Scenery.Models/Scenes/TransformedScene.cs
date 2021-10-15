// <copyright file="TransformedScene.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    /// <inheritdoc/>
    public class TransformedScene : Scene
    {
        /// <summary>
        /// Gets or sets the original scene.
        /// </summary>
        public Scene OriginalScene { get; set; } = new IcosahedronScene();
    }
}
