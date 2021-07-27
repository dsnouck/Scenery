// <copyright file="UnitedScene.cs" company="Daniel Snouck">
// Copyright (c) Daniel Snouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Scenery.Models.Scenes
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    public class UnitedScene : Scene
    {
        /// <summary>
        /// Gets the scenes to be united.
        /// </summary>
        public List<Scene> Scenes { get; set; } = new List<Scene>();
    }
}
