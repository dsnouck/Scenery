// <copyright file="IntersectedScene.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public class IntersectedScene : Scene
    {
        /// <summary>
        /// Gets the scenes to be intersected.
        /// </summary>
        public List<Scene> Scenes { get; } = new List<Scene>();
    }
}
