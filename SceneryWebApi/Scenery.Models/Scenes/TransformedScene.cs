﻿// <copyright file="TransformedScene.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
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
