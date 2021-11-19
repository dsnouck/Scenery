// <copyright file="SceneContainer.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace Scenery.Models
{
    using Scenery.Models.Scenes;

    /// <summary>
    /// Contains a <see cref="Scene"/> and everything necessary for rendering it.
    /// </summary>
    public class SceneContainer
    {
        /// <summary>
        /// Gets or sets the scene.
        /// </summary>
        public Scene Scene { get; set; } = new SphereScene();

        /// <summary>
        /// Gets or sets the projector settings.
        /// </summary>
        public ProjectorSettings Projector { get; set; } = new ProjectorSettings();

        /// <summary>
        /// Gets or sets the sampler settings.
        /// </summary>
        public SamplerSettings Sampler { get; set; } = new SamplerSettings();
    }
}
