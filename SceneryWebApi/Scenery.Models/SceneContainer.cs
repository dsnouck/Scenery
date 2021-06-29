// <copyright file="SceneContainer.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models
{
    using Scenery.Models.Scenes;

    /// <summary>
    /// Contains a <see cref="Scene"/> and everything necessary for rendering it.
    /// </summary>
    public class SceneContainer {
        /// <summary>
        /// Gets or sets the scene.
        /// </summary>
        public Scene Scene { get; set; } = new SphereScene();

        /// <summary>
        /// Gets or sets the projector settings.
        /// </summary>
        public ProjectorSettings ProjectorSettings { get; set; } = new ProjectorSettings();

        /// <summary>
        /// Gets or sets the sampler settings.
        /// </summary>
        public SamplerSettings SamplerSettings { get; set; } = new SamplerSettings();
    }
}
